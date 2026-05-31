using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    private Image img;

    private void Start()
    {
        img = GetComponent<Image>();
    }
    private void Update()
    {
        if(item.Id!=0)
        {
            img.sprite = Utils.LoadSprite(item.IconImagePath);
        }
        else
        {
            img.sprite = Utils.LoadSprite(Path.DefaultImgPath);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && item.Id != 0)
            Unload();
    }
    public void Unload()
    {
        BagManager.instance.AddItemToBag(item, 1,false);
        item=new Item();
    }
}
