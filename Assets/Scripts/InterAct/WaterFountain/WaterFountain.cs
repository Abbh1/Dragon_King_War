using UnityEngine;

public class WaterFountain : MonoBehaviour
{
    private bool canReach;
    private void Start()
    {
        if(GameController.instance!=null)
            GameController.instance.waterFountainPos = new Vector3(transform.position.x-1.0f,2.46f,transform.position.z+1.0f);
    }
    private void Update()
    {  
        if(canReach&&Input.GetKeyDown(KeyCode.F))
        {
            GameController.instance.GetPlayerState().value.presentHP = GameController.instance.GetPlayerState().value.maxHP;
            GameController.instance.GetPlayerState().value.presentMP = GameController.instance.GetPlayerState().value.maxMP;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Tags.player)
        {
            TipController.instance.AddTip("按下F喝下泉水", Tip.TipType.Middle);
            canReach = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag==Tags.player)
        {
            canReach = false;
        }
    }
}
