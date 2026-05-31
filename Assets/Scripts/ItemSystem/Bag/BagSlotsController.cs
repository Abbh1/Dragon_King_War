using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 管理背包slotUI
/// </summary>
public class BagSlotsController: MonoBehaviour
{
    public Mode mode;
    public BagSlot[] slots;
    public Text goldAmount;
    public Text level;
    public Text maxHP;
    public Text maxMP;
    public Text attack;
    public Text defence;
    public Text critChance;
    public Text critRate;
    public Text speed;
    public static BagSlotsController instance;
    public enum Mode
    {
        Use,
        ForgeSelect
    }
    private void Start()
    {
        instance = this;
        slots = GetComponentsInChildren<BagSlot>();
    }

    private void Update()
    {
        LoadBag();
        ShowPlayerInfo();
    }
    /// <summary>
    /// 根据BagManager中bag加载背包UI
    /// </summary>
    private void LoadBag()
    {
        goldAmount.text = ":" + BagManager.instance.money;
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < BagManager.instance.bag.items.Count)
                slots[i].item = BagManager.instance.bag.items[i];
            else
                slots[i].item = new Item();
        }
    }
    private void ShowPlayerInfo()
    {
        Value value = GameController.instance.GetPlayerState().value;
        level.text = "等级：" + value.level;
        maxHP.text = "血量上限：" + value.maxHP;
        maxMP.text = "魔法上限：" + value.maxMP;
        attack.text = "攻击力：" + value.attack;
        defence.text = "防御力：" + value.defence;
        critChance.text = "暴击率：" + value.critChance;
        critRate.text = "暴击伤害：" + value.critRate;
        speed.text = "速度：" + value.runSpeed;
    }
    public void ClosePanel()
    {
        foreach (BagSlot slot in slots)
        {
            slot.itemInfo.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
