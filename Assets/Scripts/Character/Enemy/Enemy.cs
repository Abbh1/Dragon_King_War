using UnityEngine;
public class Enemy : MonoBehaviour
{
    public GuardBox guardBox;
    public State state;
    public Vector3 bornPlace;
    protected float lastAttackTime;
    private float rotateMultiplier;//转向减速因子
    protected bool isAlarmed;
    protected Animator anim;
    protected Vector3 target=Vector3.zero;//前往目标坐标
    protected Behaviour behaviour;
    protected float timer;
    /// <summary>
    /// 行为数值越高优先级越高
    /// </summary>
    public enum Behaviour 
    {
        Idle=0,
        Circle=1,
        GoToTarget=2,
        Attack=3,
        Attacked=4,
        Die=5
    }
    /// <summary>
    /// 扇形攻击
    /// </summary>
    /// <param name="attacker">攻击者位置</param>
    /// <param name="target">被攻击者位置</param>
    /// <param name="angle">攻击角度</param>
    /// <param name="radius">攻击半径</param>
    /// <returns></returns>
    protected bool UmbrellaAttack(Transform attacker, Vector3 target, float angle, float radius)
    {
        Vector3 attackDir = target - attacker.position;
        float realAngle = Mathf.Acos(Vector3.Dot(attackDir.normalized, attacker.forward)) * Mathf.Rad2Deg;
        if (realAngle < angle * 0.5f && attackDir.sqrMagnitude < radius * radius)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 向目标前进
    /// </summary>
    /// <param name="_target">目标坐标</param>
    /// <param name="nearestDistance">最近距离</param>
    /// <param name="_speed">前进速度</param>
    protected void GoToTarget(Vector3 _target,float nearestDistance,float _speed)
    {
        if(Vector3.Distance(_target,transform.position)>nearestDistance)
        {
            Vector3 dir = _target- transform.position;
            Quaternion quaDir = Quaternion.LookRotation(dir);
            rotateMultiplier = (Quaternion.Angle(transform.rotation, quaDir) < 5) ? 1f : 0.5f;
            transform.rotation = Quaternion.Lerp(transform.rotation, quaDir, Time.deltaTime * state.value.rotateSpeed);
            transform.position += dir.normalized * _speed * Time.deltaTime * rotateMultiplier;
        }
    }
    /// <summary>
    /// 警戒判断
    /// </summary>
    /// <param name="target"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    protected void JudgeAlarmed(Vector3 target,float distance)
    {
        isAlarmed = Vector3.Distance(target, transform.position) < distance;
    }
    /// <summary>
    /// 绕圈行走 direction为1时顺时针为0逆时针
    /// </summary>
    /// <param name="center">中心</param>
    /// <param name="wanderSpeed">前进速度</param>
    /// <param name="radius">绕圈半径</param>
    /// <param name="direction">绕圈方向</param>
    protected void CircleWalk(Vector3 center,float wanderSpeed,float radius,int direction)
    {
        Vector3 _target;
        if (Mathf.Abs((Vector3.Distance(transform.position,center)-radius))<0.5f)
        {
            //绕圈移动
            Vector3 vertical = (transform.position - center).normalized;
            if(direction==1)
            {
                _target = new Vector3(vertical.z, 0, -vertical.x);
            }
            else
            {
                _target = new Vector3(-vertical.z, 0, vertical.x);
            }
            GoToTarget(_target+transform.position, 0f, wanderSpeed);
        }
        else
        {
            //前往圆上
            _target = center + new Vector3(radius, 0, 0);
            GoToTarget(_target, 0.1f, wanderSpeed);
        }
    }
    /// <summary>
    /// 被攻击
    /// </summary>
    protected void Attacked()
    {
        state.isAttacked = false;
        state.value.presentHP -= state.harm;
        state.harm = 0;
        Debug.Log("Enemy is Attacked");
    }
    /// <summary>
    /// 攻击开始处理 由攻击动画调用
    /// </summary>
    protected void AttackStart()
    {
        if(!TipManager.instance.bagShow)
        {
            TipController.instance.AddTip(TipManager.Block, Tip.TipType.Lasting);
            TipManager.instance.bagShow = true;
        }
        lastAttackTime = timer;
        state.canMove = false;
        Debug.Log("Enemy Attack");
    }
    /// <summary>
    /// 攻击命中判定 由攻击动画直接调用
    /// </summary>
    protected void Hit()
    {
        if (UmbrellaAttack(transform, GameController.instance.GetPlayerPosition(), 180, state.value.attackDistance))
        {
            Utils.Harm(1, state, GameController.instance.GetPlayerState());
        }
    }
    /// <summary>
    /// 攻击结束处理 由攻击动画直接调用
    /// </summary>
    protected void AttackEnd()
    {
        state.canMove = true;
    }

    /// <summary>
    /// 设置当前行为,只有优先级高才能设置
    /// </summary>
    /// <param name="_behavior"></param>
    protected void SetBehavior(Behaviour _behavior)
    {
        if (_behavior > behaviour)
        {
            behaviour = _behavior;
        }
    }

    /// <summary>
    /// 动画行为控制 根据行为设置动画
    /// </summary>
    /// <param name="_behavior">当前行为</param>
    protected void AnimtorControl(Behaviour _behavior)
    {
        switch (_behavior)
        {
            case Behaviour.GoToTarget:
                {
                    anim.SetBool("run", true);
                    anim.SetBool("walk", false);
                }
                break;
            case Behaviour.Attack:
                {
                    anim.SetTrigger("attack");
                }
                break;
            case Behaviour.Circle:
                {
                    anim.SetBool("run", false);
                    anim.SetBool("walk", true);
                }
                break;
            case Behaviour.Idle:
                {
                    anim.SetBool("run", false);
                    anim.SetBool("walk", false);
                }
                break;
            case Behaviour.Attacked:
                {
                    anim.SetTrigger("isAttacked");
                }
                break;
            case Behaviour.Die:
                {
                    anim.SetTrigger("die");
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 行为函数控制 根据行为调用对应函数
    /// </summary>
    /// <param name="_behavior">当前行为</param>
    protected void BehaviorControl(Behaviour _behavior)
    {
        switch (_behavior)
        {
            case Behaviour.Idle:
                break;
            case Behaviour.Circle:
                CircleWalk(bornPlace, state.value.walkSpeed, 5.0f, 1);
                break;
            case Behaviour.GoToTarget:
                GoToTarget(target, state.value.attackDistance-0.5f, state.value.runSpeed);
                break;
            case Behaviour.Attack:
                break;
            case Behaviour.Attacked:
                Attacked();
                break;
            default:
                break;
        }
    }
    Task task;
    /// <summary>
    /// 死亡处理
    /// </summary>
    protected void Die()
    {
        Debug.Log("Die");
        DropItems();
        DropExp();
        if (guardBox != null)
            guardBox.aliveAmount--;
        Destroy(gameObject, 0.2f);

        task = TaskController.instance.FindTaskInOpenTaskById(1);
        if(task!=null)
        {
            switch (task.PresentStage)
            {
                case 5: TaskController.instance.GoNextStage(task); break;
                default: break;
            }
        }
        task = TaskController.instance.FindTaskInOpenTaskById(2);
        if (task != null)
        {
            switch (task.PresentStage)
            {
                case 1: TaskController.instance.GoNextStage(task); break;
                default: break;
            }
        }
    }
    /// <summary>
    /// 物品掉落控制
    /// </summary>
    protected void DropItems()
    {
        int money = Utils.GetRandomIntInRange(100, 300);
        BagManager.instance.AddMoney(money);
        int itemAmount = Utils.GetRandomIntInRange(2, 4);
        Item[] dropItems = new Item[itemAmount];
        for(int i=0;i<itemAmount;i++)
        {
            Item dropItem;
            if (i == 0) dropItem = ItemManager.GetItemById(19);
            else
            {
                Item.ItemQuality quality = (Utils.GetRandomIntInRange(0, 5) < 4) ? Item.ItemQuality.Common : Item.ItemQuality.Uncommon;
                dropItem = ItemManager.GetRandomItemByQuality(quality);
            }
            GameController.instance.CreateItems(dropItem, transform.position + new Vector3(0, 1, 0));
        }
    }
    protected void DropExp()
    {
        GameController.instance.player.GetComponent<Player>().exp += 100 * state.value.level;
    }
}
