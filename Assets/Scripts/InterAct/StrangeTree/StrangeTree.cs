using UnityEngine;

public class StrangeTree : MonoBehaviour
{
    public GameObject treasureBox;
    private bool canReach;
    private bool haveGot;
    private void Update()
    {
        if(canReach&&Input.GetKeyDown(KeyCode.F))
        {
            haveGot = true;
            treasureBox.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Tags.player&&!haveGot)
        {
            canReach = true;
            TipController.instance.AddTip("°´ÏÂF¼üÍÚ¾ò", Tip.TipType.Middle);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tags.player)
        {
            canReach = false;
        }
    }
}
