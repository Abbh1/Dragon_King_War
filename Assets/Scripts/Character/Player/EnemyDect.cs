using UnityEngine;

public class EnemyDect : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag==Tags.enemy)
        {
            if(!TipManager.instance.attackShow)
            {
                TipController.instance.AddTip(TipManager.Attack, Tip.TipType.Lasting);
                TipManager.instance.attackShow = true;
            }
            if(!GameController.instance.player.GetComponent<Player>().isRun)
                GameController.instance.player.transform.LookAt(other.gameObject.transform);
        }
    }
}
