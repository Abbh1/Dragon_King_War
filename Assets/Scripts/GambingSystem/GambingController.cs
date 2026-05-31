using UnityEngine;
using UnityEngine.UI;

public class GambingController : MonoBehaviour
{
    public Image type1Img;
    public Image type2Img;
    public Text chipText;
    public Text reward;

    private int baseRate;
    private int extraRate;
    private int chip;
    private Type1 type1;
    private Type2 type2;
    private bool start;
    private bool canContinue;
    private bool endTipShow;
    private bool success;
    private int presentIndex;
    private GambingSlot[] slots;

    public enum Type1
    { None, Odd, Even }
    public enum Type2
    { None, Big, Small}

    void Start()
    {
        slots = GetComponentsInChildren<GambingSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        //确认基础倍率
        if (type1 != Type1.None && type2 != Type2.None)
            baseRate = 2;
        else if (type1 != Type1.None || type2 != Type2.None)
            baseRate = 1;
        else
            baseRate = 0;
        //确认额外倍率
        switch (presentIndex)
        {
            case 1:extraRate = 2;break;
            case 2:extraRate = 3;break;
            case 3:extraRate = 5;break;
            case 4:extraRate = 9;break;
            case 5:extraRate = 17;break;
            default:extraRate = 0;break;
        }
        //更新UI
        chipText.text = chip.ToString();
        if (success)
            reward.text = (chip * baseRate * extraRate).ToString();
        else
            reward.text = "0";
        switch(type1)
        {
            case Type1.None:type1Img.sprite = Utils.LoadSprite(Path.NoneImg);break;
            case Type1.Odd:type1Img.sprite= Utils.LoadSprite(Path.OddImg);break;
            case Type1.Even:type1Img.sprite = Utils.LoadSprite(Path.EvenImg);break;
            default:break;
        }
        switch(type2)
        {
            case Type2.None:type2Img.sprite = Utils.LoadSprite(Path.NoneImg);break;
            case Type2.Big:type2Img.sprite = Utils.LoadSprite(Path.BigImg);break;
            case Type2.Small:type2Img.sprite = Utils.LoadSprite(Path.SmallImg);break;
            default:break;
        }
        //匹配判定
        if(start)
        {
            if (Type1Match() && Type2Match())
            {
                canContinue = true;
                success = true;
            }
            else
            {
                if(!endTipShow)
                {
                    TipController.instance.AddTip("游戏结束", Tip.TipType.Middle);
                    endTipShow = true;
                }
                canContinue = false;
                success = false;
            }
        }
    }
    private bool Type1Match()
    {
        if (type1 != Type1.None)
        {
            int sum = 0;
            foreach (GambingSlot slot in slots)
                sum += slot.number;
            if ((sum % 2 == 1 && type1 == Type1.Odd) || (sum % 2 == 0 && type1 == Type1.Even))
                return true;
            else
                return false;
        }
        else
            return true;
    }
    private bool Type2Match()
    {
        if(type2!=Type2.None)
        {
            int slotNumber = 0;
            int sum = 0;
            foreach (GambingSlot slot in slots)
            {
                if (slot.number != 0)
                {
                    slotNumber++;
                    sum += slot.number;
                }
                else
                    break;
            }
            if ((sum > slotNumber * 3 && type2 == Type2.Big) || (sum <= slotNumber * 3 && type2 == Type2.Small))
                return true;
            else
                return false;
        }
        else
            return true;
    }
    public void ChangeChip(int amount)
    {
        if (!start)
        {
            chip += amount;
            if (chip < 0) chip = 0;
            if (chip > 500) { chip = 500;TipController.instance.AddTip("筹码已到最大数目", Tip.TipType.Middle); }
        }
        else
            TipController.instance.AddTip("游戏已经开始无法更改筹码", Tip.TipType.Middle);
    }
    public void Continue()
    {
        if (start)
        {
            if (canContinue)
            {
                if (presentIndex < slots.Length)
                {
                    slots[presentIndex].number = Utils.GetRandomIntInRange(1,6);
                    presentIndex ++;
                }
                else
                    TipController.instance.AddTip("骰子数目已达到上限", Tip.TipType.Middle);
            }
            else
                TipController.instance.AddTip("游戏失败，请结束游戏", Tip.TipType.Middle);
        }
        else
            TipController.instance.AddTip("请先开始游戏", Tip.TipType.Middle);
    }
    public void StartGame()
    {
        if (!start)
        {
            if (type1 != Type1.None || type2 != Type2.None)
            {
                if (chip != 0)
                {
                    if (BagManager.instance.RemoveMoney(chip))
                    {
                        start = true;
                        canContinue = true;
                        Continue();
                    }
                    else
                        TipController.instance.AddTip("当前金币不足", Tip.TipType.Middle);
                }
                else
                    TipController.instance.AddTip("筹码数目不能为0", Tip.TipType.Middle); 
            }
            else
                TipController.instance.AddTip("请选择至少一种下注类型", Tip.TipType.Middle);
        }
        else
            TipController.instance.AddTip("游戏已经开始", Tip.TipType.Middle);
    }
    public void EndGame()
    {
        if(success)
            BagManager.instance.AddMoney(chip * extraRate * baseRate);
        foreach (GambingSlot slot in slots)
            slot.number = 0;
        type1 = Type1.None;
        type2 = Type2.None;
        chip = 0;
        presentIndex = 0;
        canContinue = true;
        start = false;
        endTipShow = false;
    }
    public void SetType1(int i)
    {
        if (!start)
        {
            switch (i)
            {
                case 1: type1 = Type1.Odd; break;
                case 2: type1 = Type1.Even; break;
                default: type1 = Type1.None; break;
            }
        }
        else
            TipController.instance.AddTip("游戏已经开始无法更改下注类型", Tip.TipType.Middle);
    }
    public void SetType2(int i)
    {
        if(!start)
        {
            switch (i)
            {
                case 1: type2 = Type2.Big; break;
                case 2: type2 = Type2.Small; break;
                default: type2 = Type2.None; break;
            }
        }
        else
            TipController.instance.AddTip("游戏已经开始无法更改下注类型", Tip.TipType.Middle);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
