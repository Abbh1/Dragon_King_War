using UnityEngine;

public class Torch : MonoBehaviour
{
    public Reward.RewardQuality quality;
    public GameObject treasureBox;
    public Vector3 pos;
    private bool isOpen;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Tags.fire&&!isOpen)
        {
            isOpen = true;
            GameObject obj = Instantiate(treasureBox, pos,new Quaternion(-0.5f,0.5f,0.5f,0.5f));
            obj.GetComponent<TreasureBox>().quality = quality;
        }
    }
}
