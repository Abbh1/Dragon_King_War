using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image img;
    public Item item;
    public GameObject itemInfo;
    public Text itemName;
    public Text amount;
    public Text description;
    public Text effect;
    public Text attribute;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (item.Amount == 0)
        {
            itemInfo.SetActive(false);
            img.sprite = Utils.LoadSprite("Textures/UI/ItemIcon/0000");
        }
        else
        {
            itemName.text = item.Name;
            Utils.SetTextColorByQuality(itemName, item.Quality);
            amount.text = item.Amount.ToString();
            description.text = item.Description;
            effect.text = item.Effect;
            attribute.text = ((item.Attack > 0) ? "攻击力：" + item.Attack.ToString() + "\n" : "") + ((item.Defense > 0) ? "防御力：" + item.Defense.ToString() + "\n" : "") +
                ((item.CritChance > 0) ? "暴击率：" + item.CritChance.ToString() + "\n:" : "") + ((item.CritRate > 0) ? "暴击伤害：" + item.CritRate.ToString() + "\n" : "");
            img.sprite = Utils.LoadSprite(item.IconImagePath);
        }
    }
    /// <summary>
    /// 显示物品信息
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (item.Amount != 0)
        {
            itemInfo.SetActive(false);
            ShopSlotsController.instance.selectSlot = null;
        }
    }
    /// <summary>
    /// 关闭物品信息
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item.Amount != 0)
        {
            itemInfo.SetActive(true);
            ShopSlotsController.instance.selectSlot = GetComponent<ShopSlot>();
        }
    }

    public void PurChase()
    {
        if(item.Amount!=0)
        {
            if(BagManager.instance.RemoveMoney(item.BuyPrice))
            {
                audioSource.Play();
                ShopManager.instance.RemoveItemFromShop(item, 1);
                BagManager.instance.AddItemToBag(item, 1, true);
            }
           else
            {
                TipController.instance.AddTip("金币不足", Tip.TipType.Middle);
            }
        }
    }
}
