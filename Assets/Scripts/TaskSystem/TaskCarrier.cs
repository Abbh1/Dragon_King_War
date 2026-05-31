using UnityEngine;
/// <summary>
/// 任务检测的信息
/// </summary>
[System.Serializable]
public class Target
{
    public int id;
    public int stage;
}
public class TaskCarrier : MonoBehaviour
{
    public Target[] targets; //挂载任务id号
    public Task task;//当前服务任务;
    private void Update()
    {
        for(int i=0;i<targets.Length;i++)
        {
            Task _task = TaskController.instance.FindTaskInOpenTaskById(targets[i].id);
            if (_task != null)
            {
                if (_task.PresentStage == targets[i].stage)
                {
                    task = _task;
                    break;
                }
                else
                    task = null;
            }
            else
                task = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(task!=null&&other.gameObject.tag==Tags.player)
        {
            if(!TipManager.instance.taskShow)
            {
                TipController.instance.AddTip(TipManager.Task, Tip.TipType.Lasting);
                TipManager.instance.taskShow = true;
            }
            TaskController.instance.GoNextStage(task); 
            TaskController.instance.ShowTaskDialog(task);
        }
    }
}
