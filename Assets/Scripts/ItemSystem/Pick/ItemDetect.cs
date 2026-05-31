using System.Collections.Generic;
using UnityEngine;


public class ItemDetect : MonoBehaviour
{
    public List<GameObject> nearItemObjs;
    private void Start()
    {
        nearItemObjs = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Tags.item)
        {
            if(!TipManager.instance.PickShow)
            {
                TipController.instance.AddTip(TipManager.Pick, Tip.TipType.Lasting);
                TipManager.instance.PickShow = true;
            }
            nearItemObjs.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tags.item)
        {
            nearItemObjs.Remove(other.gameObject);
        }
    }
    private void Update()
    {
        for (int i = nearItemObjs.Count - 1; i >= 0; i--)
        {
            if (nearItemObjs[i]==null)
                nearItemObjs.RemoveAt(i);
        }
    }
}
