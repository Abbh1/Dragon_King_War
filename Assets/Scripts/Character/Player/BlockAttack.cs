using UnityEngine;

public class BlockAttack : MonoBehaviour
{
    public float force=10.0f;
    public bool isActive;
    private bool isRepel;
    private float timer;
    private float repelTime = 0.5f;
    private float startTime=-10.0f;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer - startTime > repelTime)
            isRepel = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag==Tags.enemy&&isActive)
        {
            isActive = false;
            other.gameObject.GetComponent<State>().isAttacked = true;
            Utils.Harm(4, GameController.instance.GetPlayerState(), other.gameObject.GetComponent<State>());
            isRepel = true;
            startTime = timer;
        }
        if(other.gameObject.tag==Tags.enemy&&isRepel)
        {
            Repel(other.gameObject);
        }
    }
    private void Repel(GameObject obj)
    {
        Vector3 dir = obj.transform.position - transform.position;
        if(obj.GetComponent<Rigidbody>()!=null)
            obj.GetComponent<Rigidbody>().AddForce(dir.normalized * force, ForceMode.Impulse);
    }
}
