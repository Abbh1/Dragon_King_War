using UnityEngine;

public class FireCircle : MonoBehaviour
{
    private string victimTag;//受击者标签
    private State attackerState;//攻击者数值
    private float harmSpan;//伤害间隔
    private float baseHarm;//基础伤害
    private float lastingTime;//持续时间
    private float timer;
    private float lastHarmTime;
    private SphereCollider sphereCollider;
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer - lastHarmTime > harmSpan)
        {
            sphereCollider.enabled = true;
        }
        else
            sphereCollider.enabled = false;
        if(timer>lastingTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag==victimTag)
        {
            Utils.Harm(baseHarm, attackerState, other.gameObject.GetComponent<State>());
            lastHarmTime = timer;
        }
    }

    public void Initialize(string victimTag, State attackerState, float harmSpan, float baseHarm, float lastingTime)
    {
        this.victimTag = victimTag;
        this.attackerState = attackerState;
        this.harmSpan = harmSpan;
        this.baseHarm = baseHarm;
        this.lastingTime = lastingTime;
    }
}
