using UnityEngine;

public class Fire : MonoBehaviour
{
    public Vector3[] positions;
    private int presentNumber;
    private float moveSpeed = 3.0f;
    private bool canMove;
    private void Update()
    {
        if(presentNumber<=positions.Length)
        {
            if (canMove)
                GoToPos(positions[presentNumber-1]);
        }
    }
    private void GoToPos(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        if (Vector3.Distance(pos, transform.position) < 0.1f)
            canMove = false;
        else
            transform.position += dir.normalized * moveSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Tags.player&&!canMove)
        {
            if(!TipManager.instance.fireShow)
            {
                TipController.instance.AddTip(TipManager.Fire, Tip.TipType.Lasting);
                TipManager.instance.fireShow = true;
            }
            canMove = true;
            presentNumber++;
            TipController.instance.AddTip("火焰似乎指引着什么",Tip.TipType.Slow);
        }
    }
}
