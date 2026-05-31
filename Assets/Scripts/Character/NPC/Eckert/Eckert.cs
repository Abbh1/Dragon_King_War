using UnityEngine;
public class Eckert : MonoBehaviour
{
    private float runSpeed=4.5f;
    public Vector3[] positions;
    private int nextPosIndex;
    private bool canRun;
    private CharacterController cc;
    private Animator anim;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(nextPosIndex==1&&!TipManager.instance.rollShow)
        {
            TipController.instance.AddTip(TipManager.Roll, Tip.TipType.Lasting);
            TipManager.instance.rollShow = true;
        }
        if(nextPosIndex==2&&!TipManager.instance.viewShow)
        {
            TipController.instance.AddTip(TipManager.View, Tip.TipType.Lasting);
            TipManager.instance.viewShow = true;
        }
        if (nextPosIndex < positions.Length&&canRun)
        {
            Run(positions[nextPosIndex]);
            if (Arrive(positions[nextPosIndex]))
            {
                nextPosIndex++;
            }
        }
        else
            anim.SetBool("run", false);
    }
    private void Run(Vector3 pos)
    {
        anim.SetBool("run", true);
        Vector3 dir = new Vector3(pos.x - transform.position.x, 0, pos.z - transform.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = targetRotation;
        cc.Move(dir.normalized * runSpeed * Time.deltaTime);
    }
    private bool Arrive(Vector3 pos)
    {
        if (Vector3.Distance(pos, transform.position) < 1.0f)
            return true;
        else
            return false;
    }
    private void OnTriggerStay(Collider other)
    {
        Task task = TaskController.instance.FindTaskInOpenTaskById(1);
        if (other.gameObject.tag == Tags.player&&task!=null&&task.PresentStage==2)
            canRun = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tags.player)
            canRun = false;
    }
}
