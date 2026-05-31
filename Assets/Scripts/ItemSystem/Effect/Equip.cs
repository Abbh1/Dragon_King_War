using UnityEngine;

public class Equip : MonoBehaviour
{
    public static Equip instance;
    private AudioSource audioSource;
    public  AudioClip[] clips;
    private void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void EquipWeapon(Item item)
    {
        if(!TipManager.instance.weaponSkillShow)
        {
            TipController.instance.AddTip(TipManager.WeaponSkill, Tip.TipType.Lasting);
            TipManager.instance.weaponSkillShow = true;
        }
        audioSource.clip = clips[0];
        audioSource.Play();
        TipController.instance.AddTip("装备" + item.Name, Tip.TipType.Quick);
        if (BagManager.instance.weaponSlot.item.Id != 0)
            BagManager.instance.AddItemToBag(Item.CopyItem(BagManager.instance.weaponSlot.item), 1,false);
        BagManager.instance.weaponSlot.item = item;
        BagManager.instance.RemoveItemFromBag(item, 1);
    }
    public void EquipArmor(Item item)
    {
        if(!TipManager.instance.armorSkillShow)
        {
            TipController.instance.AddTip(TipManager.ArmorSkill, Tip.TipType.Lasting);
            TipManager.instance.armorSkillShow = true;
        }
        audioSource.clip = clips[0];
        audioSource.Play();
        TipController.instance.AddTip("装备" + item.Name, Tip.TipType.Quick);
        if (BagManager.instance.armorSlot.item.Id != 0)
            BagManager.instance.AddItemToBag(Item.CopyItem(BagManager.instance.armorSlot.item), 1,false);
        BagManager.instance.armorSlot.item = item;
        BagManager.instance.RemoveItemFromBag(item, 1);
    }
    public void EquipConsumble(Item item)
    {
        audioSource.clip = clips[0];
        audioSource.Play();
        TipController.instance.AddTip("装备" + item.Name, Tip.TipType.Quick);
        if (BagManager.instance.consumbleSlot.item.Id != 0)
            BagManager.instance.AddItemToBag(Item.CopyItem(BagManager.instance.consumbleSlot.item), 1,false);
        BagManager.instance.consumbleSlot.item = item;
        BagManager.instance.RemoveItemFromBag(item, 1);
    }
}
