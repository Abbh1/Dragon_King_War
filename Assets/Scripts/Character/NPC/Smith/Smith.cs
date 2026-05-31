using UnityEngine;

public class Smith : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.player)
        {
            if(!TipManager.instance.forgeShow)
            {
                TipController.instance.AddTip(TipManager.Forge, Tip.TipType.Lasting);
                TipManager.instance.forgeShow = true;
            }
            GameController.instance.forgePanelActive = true;
            TipController.instance.AddTip("按下F键打开锻造界面", Tip.TipType.Slow);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tags.player)
            GameController.instance.forgePanelActive = false;
    }
}
