using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSlotsController : MonoBehaviour
{
    public PickSlot[] slots;
    public ItemDetect itemDect;

    private void Start()
    {
        slots = GetComponentsInChildren<PickSlot>();
    }

    private void Update()
    {
        for(int i=0;i<slots.Length;i++)
        {
            if (i < itemDect.nearItemObjs.Count)
                slots[i].itemObj = itemDect.nearItemObjs[i];
            else
                slots[i].itemObj = null;
        }
    }
}
