using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagSlot : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image img;
    public Item item;
    public GameObject itemInfo;
    public Text itemName;
    public Text amount;
    public Text description;
    public Text effect;
    public Text attribute;

    public UnityEvent leftClick;
    public UnityEvent rightClick;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        leftClick.AddListener(new UnityAction(ButtonLeftClick));
        rightClick.AddListener(new UnityAction(ButtonRightClick));
    }

    // Update is called once per frame
    void Update()
    {
        if (item.Amount == 0)
        {
            itemInfo.SetActive(false);
            img.sprite = Utils.LoadSprite("Textures/UI/ItemIcon/0000");
        }
        else
        {
            itemName.text = item.Name;
            Utils.SetTextColorByQuality(itemName, item.Quality);
            amount.text = item.Amount.ToString();
            description.text = item.Description;
            effect.text = item.Effect;
            attribute.text = ((item.Attack > 0) ? "攻击力：" + item.Attack.ToString() + "\n" : "") + ((item.Defense > 0) ? "防御力：" + item.Defense.ToString() + "\n" : "") +
                ((item.CritChance > 0) ? "暴击率：" + item.CritChance.ToString() + "\n:" : "")+((item.CritRate>0)?"暴击伤害："+item.CritRate.ToString()+"\n":"");
            img.sprite = Utils.LoadSprite(item.IconImagePath);
        }
    }
    /// <summary>
    /// 显示物品信息
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (item.Amount!=0)
            itemInfo.SetActive(false);
    }
    /// <summary>
    /// 关闭物品信息
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item.Amount!=0)
            itemInfo.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && item.Id != 0)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right && item.Id != 0)
            rightClick.Invoke();
    }
    private void ButtonLeftClick()
    {
        switch(BagSlotsController.instance.mode)
        {
            case BagSlotsController.Mode.Use:
                {
                    switch (item.Type)
                    {
                        case Item.ItemType.Consumble: Equip.instance.EquipConsumble(item); break;
                        case Item.ItemType.Weapon: Equip.instance.EquipWeapon(item); break;
                        case Item.ItemType.Armor: Equip.instance.EquipArmor(item); break;
                        default: break;
                    }
                    break;
                }
            case BagSlotsController.Mode.ForgeSelect:
                {
                    ForgeController.instance.selectedSlot.item = Item.CopyItem(item);
                    BagManager.instance.RemoveItemFromBag(item, 1);
                    GameController.instance.SetGameObjectInactive(GameController.instance.bagPanel);
                    break;
                }
        }
    }
    private void ButtonRightClick()
    {
        switch (BagSlotsController.instance.mode)
        {
            case BagSlotsController.Mode.Use:
                {
                    if (item.Type == Item.ItemType.Consumble)
                        Consume.instance.ConsumeItem(item);
                    else
                        TipController.instance.AddTip("当前物品无法使用", Tip.TipType.Middle);
                    break;
                }
            case BagSlotsController.Mode.ForgeSelect:
                {
                    break;
                }
        }
    }
}
