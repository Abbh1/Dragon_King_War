using System.Collections.Generic;
using UnityEngine;

public class GuardBox : MonoBehaviour
{
    public Reward.RewardQuality quality;
    public int amount;//守护者数目
    public int aliveAmount;//存活数目
    public GameObject enemyPrefab;
    public float spawnDistance;
    public bool canOpen;
    public bool canReach;
    private void Start()
    {
        aliveAmount = amount;
        for(int i=0;i<amount;i++)
        {
            GameObject obj=Instantiate(enemyPrefab, Utils.GetRandomPositionAroundCenter(spawnDistance, transform.position), Quaternion.identity);
            obj.GetComponent<Enemy>().guardBox = this;
            obj.GetComponent<Enemy>().bornPlace = transform.position;
        }
    }
    private void Update()
    {
        if(aliveAmount<=0)
        {
            canOpen = true;
        }
        else
        {
            canOpen = false;
        }
        if(canReach&&Input.GetKeyDown(KeyCode.F))
        {
            if(canOpen)
            {
                List<Item> list = Reward.GetRewardsByQuality(quality);
                foreach (Item dropItem in list)
                {
                    GameController.instance.CreateItems(dropItem, transform.position);
                }
                Destroy(gameObject);
            }
            else
            {
                TipController.instance.AddTip("击杀附近怪物后可开启", Tip.TipType.Quick);
            }
          
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Tags.player)
        {
            if(!TipManager.instance.guardBoxShow)
            {
                TipController.instance.AddTip(TipManager.GuardBox, Tip.TipType.Lasting);
                TipManager.instance.guardBoxShow = true;
            }
            canReach = true;
            if (canOpen)
                TipController.instance.AddTip("按下F开启" + Utils.ChangeQualityToText(quality) + "宝箱", Tip.TipType.Middle);
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
