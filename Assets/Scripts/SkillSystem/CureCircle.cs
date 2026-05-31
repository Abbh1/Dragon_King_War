using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureCircle : MonoBehaviour
{
    private string receiverTag;//接受治疗者标签
    private float baseCure;//基础治疗量
    private float extraCure;//额外百分比治疗
    private float lastingTime;//持续时间
    private float cureSpan;//治疗间隔
    private float timer;
    private float lastCureTime;
    private SphereCollider sphereCollider;
    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer - lastCureTime > cureSpan)
        {
            sphereCollider.enabled = true;
        }
        else
            sphereCollider.enabled = false;
        if (timer > lastingTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == receiverTag)
        {
            Utils.Cure(baseCure, extraCure, other.gameObject.GetComponent<State>());
            lastCureTime = timer;
        }
    }
    public void Initialize(string receiverTag, float baseCure, float extraCure, float lastingTime, float cureSpan)
    {
        this.receiverTag = receiverTag;
        this.baseCure = baseCure;
        this.extraCure = extraCure;
        this.lastingTime = lastingTime;
        this.cureSpan = cureSpan;
    }
}
