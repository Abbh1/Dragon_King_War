using UnityEngine;
using UnityEngine.UI;

public class Notify : MonoBehaviour
{
    public string content;
    private bool canOpen;

    private void Update()
    {
        if(canOpen&&Input.GetKeyDown(KeyCode.F))
        {
            GameController.instance.notifyPanel.GetComponentInChildren<Text>().text = content;
            GameController.instance.SetGameObjectActive(GameController.instance.notifyPanel);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Tags.player)
        {
            if(!TipManager.instance.notifyShow)
            {
                TipController.instance.AddTip(TipManager.Notify, Tip.TipType.Lasting);
                TipManager.instance.notifyShow = true;
            }
            canOpen = true;
            TipController.instance.AddTip("按下F查看", Tip.TipType.Middle);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tags.player)
        {
            canOpen = false;
        }
    }
}
