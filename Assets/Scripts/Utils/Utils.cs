using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static Reward;

public class Utils 
{
    public static int maxLevel = 100;
    /// <summary>
    /// 返回值位于min与max包含两端的随机整数
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
  public static int GetRandomIntInRange(int min,int max)
    {
        System.Random random = new System.Random();
        return random.Next(min, max+1);
    }

    /// <summary>
    /// 从json文件中读取字符串
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
  public static string GetJsonData(string path)
    {
        string readData;
        using (StreamReader sr = File.OpenText(path))
        {
            //数据保存
            readData = sr.ReadToEnd();
            sr.Close();
        }
        return readData;
    }
    /// <summary>
    /// 向json文件中写入类
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="path"></param>
  public static void WriteJsonData(object obj,string path)
   {
        string js = UnityEngine.JsonUtility.ToJson(obj);
        //打开或者新建文档
        using (StreamWriter sw = new StreamWriter(path))
        {
            //保存数据
            sw.WriteLine(js);
            //关闭文档
            sw.Close();
            sw.Dispose();
        }
    }
    /// <summary>
    /// 加载指定路径sprite
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
  public static Sprite LoadSprite(string path)
    {
        Texture2D texture1;
        texture1 = Resources.Load<Texture2D>(path);
        Sprite sprite = Sprite.Create(texture1, new Rect(0, 0, texture1.width, texture1.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }

    /// <summary>
    /// 根据物品品质设置文本颜色
    /// </summary>
    /// <param name="_text"></param>
    /// <param name="quality"></param>
    public static void SetTextColorByQuality(Text _text, Item.ItemQuality quality)
    {
        switch (quality)
        {
            case Item.ItemQuality.Common:
                _text.color = Color.white;
                break;
            case Item.ItemQuality.Uncommon:
                _text.color = Color.green;
                break;
            case Item.ItemQuality.Rare:
                _text.color = Color.blue;
                break;
            case Item.ItemQuality.Epic:
                _text.color = Color.magenta;
                break;
            case Item.ItemQuality.Legendary:
                _text.color = Color.yellow;
                break;
            case Item.ItemQuality.Artifact:
                _text.color = Color.red;
                break;
            case Item.ItemQuality.Undroppable:
                _text.color = Color.red;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 将item数组按类型排序
    /// </summary>
    /// <param name="items">待排序数组</param>
    /// <param name="type">该类型位于数组前面</param>
    public static void SortItemListByType(List<Item> items,Item.ItemType type)
    {
        Item temp;
        for(int i=0;i<items.Count;i++)
        {
            if (items[i].Type!=type)
            {
                int j;
                for (j = i + 1; j < items.Count; j++)
                {
                    if (items[j].Type==type)
                    {
                        temp = items[i];
                        items[i] = items[j];
                        items[j] = temp;
                        break;
                    }
                }
                if (j == items.Count-1)
                    break;
            }
        }
    }

    public static Item.ItemType ChangeIntToType(int number)
    {
        switch(number)
        {
            case 0: return Item.ItemType.Consumble;
            case 1: return Item.ItemType.Weapon;
            case 2: return Item.ItemType.Armor;
            case 3: return Item.ItemType.Material;
            default:
                Debug.LogError("参数匹配错误");
                return Item.ItemType.Consumble;
        }
    }

  /// <summary>
  /// 造成伤害
  /// </summary>
  /// <param name="baseHarm">伤害数值</param>
  /// <param name="attackerState">攻击者</param>
  /// <param name="victimState">被攻击者</param>
    public static float Harm(float baseHarm,State attackerState,State victimState)
    {
        float k1 = 50, k2 = 200;
        float x = victimState.value.defence / (victimState.value.defence + victimState.value.level * k1 + k2);
        victimState.isAttacked = true;
        float critFactor = (GetRandomIntInRange(0, 100) < attackerState.value.critChance*100) ? 1 + attackerState.value.critRate : 1;
        float harm = baseHarm * critFactor * (1 + attackerState.value.damageIncrease) * (1 - victimState.value.damageReduction) * (1 - x) * attackerState.value.attack;
        victimState.harm += (harm > 0) ? harm : 0;
        return harm;
    }
   /// <summary>
   /// 治疗
   /// </summary>
   /// <param name="baseCure">基础治疗数值</param>
   /// <param name="extraCure">额外最大生命百分比治疗量</param>
   /// <param name="receiver">接受者</param>
    public static void Cure(float baseCure,float extraCure,State receiver)
    {
        receiver.value.presentHP += baseCure + extraCure * receiver.value.maxHP;
    }
    /// <summary>
    /// 根据等级设定人物基础数值
    /// </summary>
    /// <param name="value">人物value</param>
    public static void SetBaseValueByLevel(Value value)
    {
        int x = value.level;
        value.baseHP = 100 + 5 * x + x / 10 * 20;
        value.baseMP = 80 + 4 * x + x / 10 * 10;
        value.baseAttack = 30 + 3 * x + x / 10 * 10;
        value.baseDefence = 10 + x + x / 10 * 5;
        value.baseCritRate = 0.5f;
        value.baseCritChance = 0.05f;
    }
    public static Vector3 GetRandomPositionAroundCenter(float maxDistance,Vector3 center)
    {
        float y = center.y;
        float distance = maxDistance * GetRandomIntInRange(0, 100) / 100;
        float angle = GetRandomIntInRange(0, 360);
        float x = center.x + distance * Mathf.Cos(angle/3.14f*180);
        float z = center.z + distance * Mathf.Sin(angle/3.14f*180);
        return new Vector3(x, y, z);
    }

    public static string ChangeQualityToText(RewardQuality quality)
    {
        switch (quality)
        {
            case RewardQuality.Common: return "常见的";
            case RewardQuality.Uncommon: return "稀有的";
            case RewardQuality.Rare: return "罕见的";
            case RewardQuality.Epic: return "史诗的";
            default: return null;
        }
    }
    /// <summary>
    /// 根据人物当前经验返回人物等级以及溢出经验
    /// </summary>
    /// <param name="exp"></param>
    /// <returns>等级，溢出经验</returns>
    public static int[] GetLevelByExp(int exp)
    {
        int level;
        for(level=0;level<maxLevel;level++)
        {
            exp -= GetExpByLevel(level);
            if(exp<0)
            {
                exp += GetExpByLevel(level);
                level--;
                break;
            }
        }
        return new int[2] { level, exp };
    }
    /// <summary>
    /// 从level-1升级到level所需要经验
    /// </summary>
    /// <param name="level">等级</param>
    /// <returns></returns>
    public static int GetExpByLevel(int level)
    {
        return level * 100;
    }
    /// <summary>
    /// 读取键盘输入
    /// </summary>
    /// <param name="keyCode">键盘key值</param>
    /// <param name="keyCode1">SettingManager对应值</param>
    /// <returns></returns>
    public static bool GetKeyDown(KeyCode keyCode,KeyCode keyCode1)
    {
        return (Input.GetKeyDown(keyCode) && SettingManager.instance == null) || Input.GetKeyDown(keyCode1);
    }
}

