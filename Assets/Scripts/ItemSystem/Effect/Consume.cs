using UnityEngine;
public class Consume : MonoBehaviour
{
    public static Consume instance;
    private AudioSource audioSource;
    public AudioClip[] clips;
    private void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void ConsumeItem(Item item)
    {
        TipController.instance.AddTip("ÏûºÄÎïÆ·" + item.Name, Tip.TipType.Middle);
        BagManager.instance.RemoveItemFromBag(item, 1);
        Value value = GameController.instance.GetPlayerState().value;
        switch (item.Id)
        {
            case 1: value.presentHP += item.Recovery;break;
            case 2: value.presentMP += item.Recovery;break;
            case 3: value.presentHP *= 0.9f;BuffController.instance.AddBuff(new Buff(20, value.baseAttack * 0.1f, 0, 0, 0, 0, 0, 0, 0, 0));break;
            case 4:BuffController.instance.AddBuff(new Buff(60, 0, 0, 0.5f, -0.2f, 0, 0, 0, 0, 0));break;
            case 5:BuffController.instance.AddBuff(new Buff(30, 0, 0, 0, 0, 0.2f, 0.5f, 0, 0, 0));break;
            case 6:BuffController.instance.AddBuff(new Buff(40, 0, 0, 0, 0, 0, 0, 0.05f, 0, 0));break;
        }
        audioSource.clip = clips[0];
        audioSource.Play();
    }
}
