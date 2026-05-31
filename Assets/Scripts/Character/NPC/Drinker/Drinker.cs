using UnityEngine;

public class Drinker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.player)
        {
            if (!TipManager.instance.gamblingShow)
            {
                TipController.instance.AddTip(TipManager.Gambling, Tip.TipType.Lasting);
                TipManager.instance.gamblingShow = true;
            }
            GameController.instance.gamblingPanelActive = true;
            TipController.instance.AddTip("按下F键进行游戏", Tip.TipType.Slow);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tags.player)
            GameController.instance.gamblingPanelActive = false;
    }
}
