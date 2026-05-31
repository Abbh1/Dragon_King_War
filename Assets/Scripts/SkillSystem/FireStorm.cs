using UnityEngine;
public class FireStorm : MonoBehaviour
{
    private Vector3 moveDir;
    private float moveSpeed;
    private float attackSpan;
    private float lastingTime;
    private float baseharm;
    private string victimTag;
    private State attackerState;
    private float timer;
    private float lastAttackTime;
    private CapsuleCollider capsuleCollider;
    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        if(timer-lastAttackTime>attackSpan)
        {
            capsuleCollider.enabled = true;
        }
        if (timer > lastingTime)
            Destroy(gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag==victimTag)
        {
            Utils.Harm(baseharm, attackerState,other.gameObject.GetComponent<State>());
            lastAttackTime = timer;
        }
    }
    public void Initialize(Vector3 moveDir, float moveSpeed, float attackSpan, float lastingTime, float baseharm, string victimTag, State attackerState)
    {
        this.moveDir = moveDir;
        this.moveSpeed = moveSpeed;
        this.attackSpan = attackSpan;
        this.lastingTime = lastingTime;
        this.baseharm = baseharm;
        this.victimTag = victimTag;
        this.attackerState = attackerState;
    }
}
