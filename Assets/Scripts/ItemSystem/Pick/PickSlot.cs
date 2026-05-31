using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class PickSlot : MonoBehaviour
{
    public GameObject itemObj;
    public Text itemName;
    private AudioSource audioSource;
    public Image img;
    private Item item;
    private void Start()
    {
        img = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
    }
    public void onPickUp()
    {
        if(itemObj!=null)
        {
            audioSource.Play();
            BagManager.instance.AddItemToBag(item,1,true);
            Destroy(itemObj);
        }
    }

    private void Update()
    {
        if(itemObj!=null)
        {
            item = itemObj.GetComponent<ItemObject>().item;
        }
        ShowItemName();
        ShowSlotImg();
    }

    void ShowItemName()
    {
        if(itemObj!=null)
        {
            itemName.text = item.Name;
        }
        else
        {
            itemName.text = "";
        }
    }
    void ShowSlotImg()
    {
        if(itemObj!=null)
        {
            img.sprite = Utils.LoadSprite("Textures/UI/PickPanel/PickSlot");
        }
        else
        {
            img.sprite = Utils.LoadSprite("Textures/UI/PickPanel/Null");
        }
    }
}
