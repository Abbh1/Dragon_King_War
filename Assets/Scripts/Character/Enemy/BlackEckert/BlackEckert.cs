using UnityEngine;

public class BlackEckert : MonoBehaviour
{
    public Behaviour behaviour;
    public GameObject normalAttack;
    public float blockSpan = 5.0f;
    private float lastAttackTime;
    private float lastBlockTime;
    public int attackIndex=1;
    private float timer;
    private Animator anim;
    private State state;
    private CharacterController cc;
    public  enum Behaviour
    {
       FightIdle,
       Run,
       CircleWalk,
       Attack,
       Attacked,
       Block,
    }

    private void SetBehaviour(Behaviour _behaviour)
    {
        if (_behaviour > behaviour)
            behaviour = _behaviour;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        state = GetComponent<State>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        behaviour = Behaviour.FightIdle;
        if(Vector3.Distance(transform.position,GameController.instance.GetPlayerPosition())>state.value.alarmDistance)
        {
            SetBehaviour(Behaviour.Run);
        }
        else
        {
            SetBehaviour(Behaviour.CircleWalk);
            if(Vector3.Distance(transform.position,GameController.instance.GetPlayerPosition())<state.value.attackDistance&&timer-lastAttackTime>state.value.attackSpan
                &&Vector3.Dot(GameController.instance.GetPlayerPosition()-transform.position,transform.forward)>0)
            {
                SetBehaviour(Behaviour.Attack);
            }
        }
        if(GameController.instance.player.GetComponent<Player>().isAttack&&timer-lastBlockTime>blockSpan)
        {
            SetBehaviour(Behaviour.Block);
        }
        if (state.isAttacked)
            SetBehaviour(Behaviour.Attacked);

        AnimtorControl(behaviour);
        BehaviorControl(behaviour);
    }
    private void GoToPos(Vector3 pos,float nearestDistance,float speed)
    {
        if (Vector3.Distance(pos, transform.position) > nearestDistance&&state.canMove)
        {
            Vector3 dir = new Vector3(pos.x - transform.position.x, 0, pos.z - transform.position.z);
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, state.value.rotateSpeed * Time.deltaTime);
            cc.Move(dir.normalized * speed * Time.deltaTime);
        }
    }
    private void CircleWalk(Vector3 center,float speed,int direction)
    {
        Vector3 _target;
        Vector3 vertical = (transform.position - center).normalized;
        if (direction == 1)
        {
            _target = new Vector3(vertical.z, 0, -vertical.x);
        }
        else
        {
            _target = new Vector3(-vertical.z, 0, vertical.x);
        }
        GoToPos(_target + transform.position, 0f, speed);
        transform.LookAt(GameController.instance.player.transform);
    }
    #region ¹¥»÷Ïà¹Øº¯Êý
    private void Attacked()
    {
        state.isAttacked = false;
        state.value.presentHP -= state.harm;
        state.harm = 0;
        Debug.Log("Enemy is Attacked");
    }
    private bool UmbrellaAttack(Transform attacker, Vector3 target, float angle, float radius)
    {
        Vector3 attackDir = target - attacker.position;
        float realAngle = Mathf.Acos(Vector3.Dot(attackDir.normalized, attacker.forward)) * Mathf.Rad2Deg;
        if (realAngle < angle * 0.5f && attackDir.sqrMagnitude < radius * radius)
        {
            return true;
        }
        return false;
    }
    public void AttackStart()
    {
        state.canMove = false;
    }
    public void Hit()
    {
        if(UmbrellaAttack(transform,GameController.instance.GetPlayerPosition(),180,state.value.attackDistance))
        {
            Utils.Harm(1, state, GameController.instance.GetPlayerState());
        }
    }
    public void AttackEnd()
    {
        state.canMove = true;
    }
    public void Block()
    {
        state.isAttacked = false;
        lastBlockTime = timer;
        Debug.Log("block");
    }
    #endregion



    private void AnimtorControl(Behaviour _behaviour)
    {
        switch(_behaviour)
        {
            case Behaviour.FightIdle: break;
            case Behaviour.Run:anim.SetBool("run", true);anim.SetBool("circleWalk",false); break;
            case Behaviour.CircleWalk:anim.SetBool("run", false);anim.SetBool("circleWalk", true);break;
            case Behaviour.Attack:anim.SetTrigger("attack"+attackIndex);break;
            case Behaviour.Attacked:anim.SetTrigger("attacked");break;
            case Behaviour.Block:anim.SetTrigger("block");break;
        }
    }
    private void BehaviorControl(Behaviour _behaviour)
    {
        switch(_behaviour)
        {
            case Behaviour.FightIdle:break;
            case Behaviour.Run:GoToPos(GameController.instance.GetPlayerPosition(),state.value.alarmDistance, state.value.runSpeed);break;
            case Behaviour.CircleWalk:if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1")&& !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2")&& !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3")) CircleWalk(GameController.instance.GetPlayerPosition(), state.value.walkSpeed, -1);break;
            case Behaviour.Attack: lastAttackTime = timer; attackIndex = attackIndex % 3; attackIndex++; break;
            case Behaviour.Attacked:Attacked();break;
            case Behaviour.Block:Block();break;
        }
    }
}
