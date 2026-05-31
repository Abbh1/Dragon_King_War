/// <summary>
/// Item只能由ItemManager合法生成
/// </summary>
[System.Serializable]
public class Item 
{
    public int Id;//Id大于0时合法
    public string Name;
    public ItemQuality Quality;
    public ItemType Type;
    public int Attack;//攻击力
    public int Defense;//防御力
    public int Recovery;//回复量
    public float CritChance;//暴击率
    public float CritRate;//暴击伤害
    public string Effect;//特效描述
    public int Amount;
    public int BuyPrice;
    public string IconImagePath;
    public string Description;

   public enum ItemQuality
   {
        Common,//常见
        Uncommon,//稀有
        Rare,//罕见
        Epic,//史诗
        Legendary,//传奇
        Artifact,//神器
        Undroppable//不可掉落
   }

    public enum ItemType
    {
        Consumble,
        Weapon,
        Armor,
        Material
    }
    public Item() { }

    public Item(int id, string name, ItemQuality quality, ItemType type, int attack, int defense, int recovery, float critChance, float critRate, string effect, int amount, int buyPrice,string iconImagePath, string description)
    {
        Id = id;
        Name = name;
        Quality = quality;
        Type = type;
        Attack = attack;
        Defense = defense;
        Recovery = recovery;
        CritChance = critChance;
        CritRate = critRate;
        Effect = effect;
        Amount = amount;
        BuyPrice = buyPrice;
        IconImagePath = iconImagePath;
        Description = description;
    }

    public static Item CopyItem(Item item)
    {
        return new Item(item.Id, item.Name, item.Quality, item.Type,item.Attack,item.Defense,item.Recovery,item.CritChance,item.CritRate,item.Effect, item.Amount, item.BuyPrice,item.IconImagePath, item.Description);
    }
}
