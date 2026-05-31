using UnityEngine;

public class LightingExplode : MonoBehaviour
{
    private string victimTag;//受击者标签
    private State attackerState;//攻击者数值
    private float baseHarm;//基础伤害
    private float lastingTime;//持续时间
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>lastingTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag==victimTag)
        {
            Utils.Harm(baseHarm, attackerState, other.gameObject.GetComponent<State>());
        }
    }

    public void Initialize(string victimTag, State attackerState, float baseHarm, float lastingTime)
    {
        this.victimTag = victimTag;
        this.attackerState = attackerState;
        this.baseHarm = baseHarm;
        this.lastingTime = lastingTime;
    }
}
