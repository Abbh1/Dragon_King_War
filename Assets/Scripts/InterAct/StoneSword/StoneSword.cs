using UnityEngine;
public class StoneSword : MonoBehaviour
{
    public GameObject enemy;
    public GameObject sword;
    private bool canGet;
    private bool canReach;
    private bool haveGot;
    private float pressTime=1.5f;
    public float timer;
    private bool start;
    private void Update()
    {
        Task task = TaskController.instance.FindTaskInOpenTaskById(1);
        if (task != null && task.PresentStage == 4)
            canGet = true;
        if (canReach&&Input.GetKeyDown(KeyCode.F))
            start = true;
        if (Input.GetKeyUp(KeyCode.F))
            start = false;
        if (start)
            timer += Time.deltaTime;
        else
            timer = 0;
        if(timer>pressTime&&!haveGot)
            GetSword();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag==Tags.player&&!haveGot&&canGet)
        {
            TipController.instance.AddTip("³¤°´F°Î³ö½£", Tip.TipType.Slow);
            canReach = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tags.player)
        {
            canReach = false;
        }
    }
    private void GetSword()
    {
        sword.SetActive(false);
        BagManager.instance.AddItemToBag(ItemManager.GetItemById(13), 1, true);
        if(!TipManager.instance.bagShow)
        {
            TipController.instance.AddTip(TipManager.Bag, Tip.TipType.Lasting);
            TipManager.instance.bagShow = true;
        }
        haveGot = true;
        Task task = TaskController.instance.FindTaskInOpenTaskById(1);
        if (task != null)
        {
            switch (task.PresentStage)
            {
                case 4: TaskController.instance.GoNextStage(task); TaskController.instance.ShowTaskDialog(task); break;
                default: break;
            }
        }
        GameObject obj=Instantiate(enemy, transform.position+new Vector3(-1.75f,0.81f,-8.0f), Quaternion.identity);
        obj.GetComponent<Skeleton>().bornPlace = transform.position;
    }
}
