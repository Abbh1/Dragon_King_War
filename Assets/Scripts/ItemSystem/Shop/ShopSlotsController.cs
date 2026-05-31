using UnityEngine;
using UnityEngine.UI;

public class ShopSlotsController : MonoBehaviour
{
    public ShopSlot[] slots;
    public Item.ItemType showType;
    public bool isSelect;//筛选开启标志
    public static ShopSlotsController instance;
    public ShopSlot selectSlot;//选中格子
    public Text traderTalk;

    private void Start()
    {
        slots = GetComponentsInChildren<ShopSlot>();
        instance = this;
    }
    private void Update()
    {
        LoadShop();
        UpdateText();
    }
    private void LoadShop()
    {
        Utils.SortItemListByType(ShopManager.instance.shop.items, showType);
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < ShopManager.instance.shop.items.Count && (ShopManager.instance.shop.items[i].Type==showType||!isSelect))
                slots[i].item = ShopManager.instance.shop.items[i];
            else
                slots[i].item = new Item();
        }
    }
    public void ChangeShowType(int number)
    {
        showType=Utils.ChangeIntToType(number);
        isSelect = true;
    }
    public void ShowAllItems()
    {
        isSelect = false;
    }

    private void UpdateText()
    {
        if (selectSlot != null)
        {
            traderTalk.text = "这件商品价格是："+selectSlot.item.BuyPrice;
        }
        else
        {
            traderTalk.text = "想买些什么";
        }
    }
}
