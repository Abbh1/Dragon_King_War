using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public int harm;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Tags.enemy)
        {
            float damage = Utils.Harm(harm, GameController.instance.GetPlayerState(), other.gameObject.GetComponent<State>());
            Utils.Cure(damage * BuffController.instance.tempAbsorbRate, 0, GameController.instance.GetPlayerState());
            GameController.instance.player.GetComponent<Player>().isAttack = true;
        }
    }
}
