using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存储所有item信息
/// </summary>
[System.Serializable]
public class ItemsInformation
{
    public Item[] items;
}

/// <summary>
/// 管理item信息
/// </summary>
public class ItemManager
{
    /// <summary>
    /// 获取物品信息类
    /// </summary>
    /// <returns></returns>
    public static ItemsInformation GetItemsInformation()
    {
        string jsonData=Utils.GetJsonData(Path.ItemInfoPath);
        ItemsInformation info = JsonUtility.FromJson<ItemsInformation>(jsonData);
        return info;
    }
    /// <summary>
    /// 根据json文件生成指定id amount为0的item
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Item  GetItemById(int id)
    {
        ItemsInformation info = GetItemsInformation();
        foreach(Item item in info.items)
        {
            if(item.Id==id)
            {
                return Item.CopyItem(Item.CopyItem(item));
            }
        }
        Debug.Log("no such id item");
        return null;
    }
    public static List<Item> GetItemsByQuality(Item.ItemQuality quality)
    {
        List<Item> list = new List<Item>();
        ItemsInformation info = GetItemsInformation();
        foreach (Item item in info.items)
        {
           if(item.Quality==quality)
            {
                list.Add(item);
            }
        }
        return list;
    }
    public static Item GetRandomItemByQuality(Item.ItemQuality quality)
    {
        List<Item> items = GetItemsByQuality(quality);
        if (items.Count > 0)
        {
            int number = Utils.GetRandomIntInRange(0, items.Count-1);
            return items[number];
        }
        else
            return null;
    }
}

