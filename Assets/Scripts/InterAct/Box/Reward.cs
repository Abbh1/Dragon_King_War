using System.Collections.Generic;

public class Reward
{
   public enum RewardQuality
    {
        Common,//常见
        Uncommon,//稀有
        Rare,//罕见
        Epic,//史诗
    }

   public static List<Item> GetRewardsByQuality(RewardQuality quality)
    {
        List<Item> list = new List<Item>();
        switch(quality)
        {
            case RewardQuality.Common:
                {
                    BagManager.instance.AddMoney(20);
                    int amount = Utils.GetRandomIntInRange(2, 3);
                    for(int i=0;i<amount;i++)
                    {
                        list.Add(ItemManager.GetRandomItemByQuality(Item.ItemQuality.Common));
                    }
                }
                break;
            case RewardQuality.Uncommon:
                {
                    BagManager.instance.AddMoney(50);
                    int amount = Utils.GetRandomIntInRange(1, 2);
                    for (int i = 0; i < amount; i++)
                    {
                        list.Add(ItemManager.GetRandomItemByQuality(Item.ItemQuality.Uncommon));
                    }
                }
                break;
            case RewardQuality.Rare:
                {
                    BagManager.instance.AddMoney(100);
                    int amount = 1;
                    for (int i = 0; i < amount; i++)
                    {
                        list.Add(ItemManager.GetRandomItemByQuality(Item.ItemQuality.Rare));
                    }
                }
                break;
            case RewardQuality.Epic:
                {
                    BagManager.instance.AddMoney(200);
                    int amount = 1;
                    for (int i = 0; i < amount; i++)
                    {
                        list.Add(ItemManager.GetRandomItemByQuality(Item.ItemQuality.Epic));
                    }
                }
                break;
            default:break;
        }
        return list;
    }
}
