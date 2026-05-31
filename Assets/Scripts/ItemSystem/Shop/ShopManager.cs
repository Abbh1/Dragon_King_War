using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储商店中商品信息
/// </summary>
[System.Serializable]
public class Shop
{
    public List<Item> items;
}
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public Shop shop;

    private void Start()
    {
        instance = this;
        LoadShopFromJson();
    }

    private void Update()
    {

    }
    public void SaveShopToJson()
    {
        Utils.WriteJsonData(shop, Path.ShopPath);
    }

    public void LoadShopFromJson()
    {
        string jsonData = Utils.GetJsonData(Path.ShopPath);
        shop = JsonUtility.FromJson<Shop>(jsonData);
    }

    public void AddItemToShop(Item item, int amount)
    {
        bool exist = false;
        foreach (Item _item in shop.items)
        {
            if (_item.Id == item.Id)
            {
                _item.Amount += amount;
                exist = true;
            }
        }
        if (!exist)
        {
            Item _item = Item.CopyItem(item);
            _item.Amount = amount;
            shop.items.Add(_item);
        }
    }

    public void RemoveItemFromShop(Item item, int amount)
    {
        bool exist = false;
        for (int i = shop.items.Count - 1; i >= 0; i--)
        {
            if (shop.items[i].Id == item.Id)
            {
                exist = true;
                shop.items[i].Amount -= amount;
                if (shop.items[i].Amount <= 0)
                    shop.items.RemoveAt(i);
            }
        }
        if (!exist)
            Debug.Log("no such item in shop");
    }
}
