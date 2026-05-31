using UnityEngine;

public class NPC : MonoBehaviour
{
    public Vector3[] positions;
    public float speed=2.0f;
    private bool canMove=true;
    private int index;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    { 
        if (!Arrive(positions[index]))
        {
            if (canMove)
                Run(positions[index]);
            else
                Idle();
        }
        else
        {
            if (index < positions.Length-1)
                index++;
            else
                index = 0;
        }
    }
    /// <summary>
    /// 前往指定位置
    /// </summary>
    /// <param name="pos"></param>
    private void Run(Vector3 pos)
    {
        anim.SetBool("walk", true);
        Vector3 dir = new Vector3(pos.x - transform.position.x, 0, pos.z - transform.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = targetRotation;
        transform.position += dir.normalized * speed * Time.deltaTime;
    }
    private bool Arrive(Vector3 pos)
    {
        if (Vector3.Distance(pos, transform.position) < 1.0f)
            return true;
        else
            return false;
    }
    private void Idle()
    {
        anim.SetBool("walk", false);
        transform.LookAt(GameController.instance.player.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Tags.player)
        {
            canMove = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag==Tags.player)
        {
            canMove = true;
        }
    }
}
