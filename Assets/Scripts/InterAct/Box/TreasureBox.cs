using System.Collections.Generic;
using UnityEngine;
public class TreasureBox : MonoBehaviour
{
    private bool canOpen;
    public Reward.RewardQuality quality;
    private void Update()
    {
        if(canOpen&&Input.GetKeyDown(KeyCode.F))
        {
            List<Item> list = Reward.GetRewardsByQuality(quality);
            foreach(Item dropItem in list)
            {
                GameController.instance.CreateItems(dropItem, transform.position);
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Tags.player)
        {
            if(!TipManager.instance.treasureBoxShow)
            {
                TipController.instance.AddTip(TipManager.TreasureBox, Tip.TipType.Lasting);
                TipManager.instance.treasureBoxShow = true;
            }
            TipController.instance.AddTip("按下F打开"+Utils.ChangeQualityToText(quality)+"宝箱", Tip.TipType.Middle);
            canOpen = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag==Tags.player)
        {
            canOpen = false;
        }
    }
}
