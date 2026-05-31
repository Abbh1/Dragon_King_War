using UnityEngine;
public class TipManager : MonoBehaviour
{

    public static TipManager instance;
    public static string Run = "按下WASD键控制人物移动";
    public bool runShow;
    public static string Roll = "按下Shift键进行翻滚更快移动";
    public bool rollShow;
    public static string View = "按下鼠标右键滑动调整视角，使用鼠标滚轮调整距离";
    public bool viewShow;
    public static string Attack = "按下J键对敌人进行攻击，合适时机再次按下进行更高伤害连击";
    public bool attackShow;
    public static string Block = "长按H键进行招架，合适时机点按弹反敌人攻击";
    public bool blockShow;
    public static string Bag = "按下B键打开背包，左键装备武器，右键消耗药品";
    public bool bagShow;
    public static string WeaponSkill = "当武器附带技能时按下E键消耗一定MP释放技能";
    public bool weaponSkillShow;
    public static string ArmorSkill = "当防具附带技能时按下R键消耗一定MP释放技能";
    public bool armorSkillShow;
    public static string Forge = "在合成界面点击空白材料格后在背包中选中原料，满足配方后点击转化按钮合成物品";
    public bool forgeShow;
    public static string Shop = "在商店界面花费金币向商人购买商品";
    public bool shopShow;
    public static string Gambling = "在与酒鬼进行游戏获得金币";
    public bool gamblingShow;
    public static string Task = "按下V键打开任务列表，查看任务信息";
    public bool taskShow;
    public static string Fire = "跟随野外的火焰或许能够找到什么";
    public bool fireShow;
    public static string TreasureBox = "野外遗落许多宝箱，按下F键开启";
    public bool treasureBoxShow;
    public static string GuardBox = "被魔物守护的宝箱，击杀它们后按下F键开启";
    public bool guardBoxShow;
    public static string Notify = "随处可见的告示牌可能藏有不为人知的秘密，按下F查看";
    public bool notifyShow;
    public static string Pick = "开启宝箱或者击杀怪物掉落的物品在一定距离内可点击右侧拾取栏拾取";
    public bool PickShow;
    void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if(!runShow)
        {
            TipController.instance.AddTip(Run, Tip.TipType.Lasting);
            instance.runShow = true;
        }
    }
}
