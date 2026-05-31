using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipController : MonoBehaviour
{
    public static TipController instance;
    public GameObject tipPrefab;
    public GameObject lastingTipPrefab;
    private Queue<GameObject> tips = new Queue<GameObject>();
    private GameObject tip;

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        ShowTip();
    }
    public void AddTip(string info,Tip.TipType tipType)
    {
        if(tipType==Tip.TipType.Lasting)
        {
            GameObject obj = GameObject.Find("LastingTipPanel");
            var lastingTip = Instantiate(lastingTipPrefab,obj.transform);
            lastingTip.GetComponent<Tip>().content.text = info;
            lastingTip.GetComponent<Tip>().type = tipType;
            tips.Enqueue(lastingTip);
        }
        else
        {
            var tip = Instantiate(tipPrefab, transform);
            tip.GetComponent<Tip>().content.text = info;
            tip.GetComponent<Tip>().type = tipType;
            tips.Enqueue(tip);
        }
    }

    private void ShowTip()
    {
        if(tip==null&&tips.Count!=0)
        {
            tip = tips.Dequeue();
            tip.SetActive(true);
        }
    }
}
