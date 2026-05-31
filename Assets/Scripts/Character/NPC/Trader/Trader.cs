using UnityEngine;

public class Trader : MonoBehaviour
{
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Tags.player)
        {
            if(!TipManager.instance.shopShow)
            {
                TipController.instance.AddTip(TipManager.Shop, Tip.TipType.Lasting);
                TipManager.instance.shopShow = true;
            }
            GameController.instance.shopPanelActive = true;
            TipController.instance.AddTip("∞¥œ¬Fº¸‰Ø¿¿…ÃµÍ", Tip.TipType.Slow);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag==Tags.player)
            GameController.instance.shopPanelActive = false;
    }
}
