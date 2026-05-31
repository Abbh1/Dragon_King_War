using System.Collections.Generic;
using UnityEngine;
#region 可序列化数据类
/// <summary>
/// 存储背包中物品信息
/// </summary>
[System.Serializable]
public class Bag
{
    public List<Item> items;
}
/// <summary>
/// 储存背包中装备信息
/// </summary>
[System.Serializable]
public class Equipment
{
    public Item weapon;
    public Item armor;
    public Item consumble;

    public Equipment(Item weapon, Item armor, Item consumble)
    {
        this.weapon = weapon;
        this.armor = armor;
        this.consumble = consumble;
    }
}
/// <summary>
/// 储存背包中金币信息
/// </summary>
[System.Serializable]
public class Money
{
    public int amount;
    public Money(int amount)
    {
        this.amount = amount;
    }
}
#endregion
/// <summary>
/// 在数据层面更改背包物品信息
/// </summary>
public class BagManager :MonoBehaviour
{
    public Bag bag;
    public static BagManager instance;
    public EquipmentSlot weaponSlot;
    public EquipmentSlot armorSlot;
    public EquipmentSlot consumbleSlot;
    public int money;

    private void Start()
    {
        instance = this;
        if (SceneController.instance != null && !SceneController.instance.isNewGame)
        {
            LoadBagFromJson();
            LoadEquipmentFromJson();
            LoadMoneyFromJson();
        }
    }
    private void Update()
    {
        Task task = TaskController.instance.FindTaskInOpenTaskById(3);
        if(task!=null&&task.PresentStage==1)
        {
            Item item = FindItemInBag(ItemManager.GetItemById(26));
            if(item!=null&&item.Amount>=2)
            {
                TaskController.instance.GoNextStage(task);
            }
        }
    }
    #region 保存信息
    /// <summary>
    /// 将背包信息保存为json文件
    /// </summary>
    public void SaveBagToJson()
    {
        Utils.WriteJsonData(bag, Path.BagPath);
    }
    public void SaveEquipmentToJson()
    {
        Equipment equipment = new Equipment(weaponSlot.item, armorSlot.item, consumbleSlot.item);
        Utils.WriteJsonData(equipment, Path.EquipmentPath);
    }
    public void SaveMoneyToJson()
    {
        Money _money = new Money(money);
        Utils.WriteJsonData(_money, Path.MoneyPath);
    }
    #endregion
    #region 载入信息
    /// <summary>
    /// 从json文件中加载背包
    /// </summary>
    public void LoadBagFromJson()
    {
        string jsonData = Utils.GetJsonData(Path.BagPath);
        bag = JsonUtility.FromJson<Bag>(jsonData);
    }
    public void LoadEquipmentFromJson()
    {
        string jsonData = Utils.GetJsonData(Path.EquipmentPath);
        Equipment equipment= JsonUtility.FromJson<Equipment>(jsonData);
        weaponSlot.item = equipment.weapon;
        armorSlot.item = equipment.armor;
        consumbleSlot.item = equipment.consumble;
    }
    public void LoadMoneyFromJson()
    {
        string jsonData = Utils.GetJsonData(Path.MoneyPath);
        Money _money = JsonUtility.FromJson<Money>(jsonData);
        money = _money.amount;
    }
    #endregion
    #region 修改背包属性
    /// <summary>
    /// 向背包中添加指定数量物品
    /// </summary>
    /// <param name="item">物品</param>
    /// <param name="amount">数量</param>
    public void AddItemToBag(Item item,int amount,bool showTip)
    {
        if(showTip)
        {
            string info = "获得" + item.Name + " X " + amount;
            TipController.instance.AddTip(info, Tip.TipType.Quick);
        }
        bool exist = false;
        foreach(Item _item in bag.items)
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
            bag.items.Add(_item);
        }
    }
    /// <summary>
    /// 从背包中移除物品
    /// </summary>
    /// <param name="item">物品</param>
    /// <param name="amount">数目</param>
    public void RemoveItemFromBag(Item item,int amount)
    {
        bool exist = false;
        for (int i = bag.items.Count - 1; i >= 0; i--)
        {
            if (bag.items[i].Id==item.Id)
            {
                exist = true;
                bag.items[i].Amount -= amount;
                if (bag.items[i].Amount <= 0)
                    bag.items.RemoveAt(i);
            }
        }
        if (!exist)
            Debug.Log("no such item in bag");
    }

    public void AddMoney(int amount)
    {
        money += amount;
        TipController.instance.AddTip("获得金币X" + amount, Tip.TipType.Quick);
    }
    public bool RemoveMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        else
            return false;
    }
    #endregion
    public Item FindItemInBag(Item item)
    {
        for(int i=0;i<bag.items.Count;i++)
        {
            if (item.Id == bag.items[i].Id)
                return Item.CopyItem(bag.items[i]);
        }
        return null;
    }
}
