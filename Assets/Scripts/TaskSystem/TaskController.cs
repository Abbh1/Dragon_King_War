using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class OpenTask
{
    public List<Task> tasks;//记录当前已开放任务
}

public class TaskController : MonoBehaviour
{
    public OpenTask openTask;
    public static TaskController instance;
    private Task destoryTask;

    private void Start()
    {
        instance = this;
        if (SceneController.instance!=null&&!SceneController.instance.isNewGame)
            LoadTaskFromJson();
        else
            AddTaskToOpenTasks(TaskManager.GetTaskById(1));
    }
    /// <summary>
    /// 任务开启时调用
    /// </summary>
    /// <param name="task"></param>
    public void AddTaskToOpenTasks(Task task)
    {
        openTask.tasks.Add(task);
    }
    /// <summary>
    /// 任务完成时调用
    /// </summary>
    /// <param name="task"></param>
    public void RemoveTaskFromTasks(Task task)
    {
        Debug.Log(task.Title + "finish");

        GetTaskRewards(task);

        for(int i=0;i<task.NextTask.Length;i++)
        {
            AddTaskToOpenTasks(TaskManager.GetTaskById(task.NextTask[i]));
        }

        for (int i = openTask.tasks.Count - 1; i >= 0; i--)
        {
            if (openTask.tasks[i].Id == task.Id)
            {
                openTask.tasks.RemoveAt(i);
            }
        }
        if(TaskBar.instance!=null)
          TaskBar.instance.selectedTask = null;
    }
    /// <summary>
    /// 任务进入下一阶段
    /// </summary>
    /// <param name="task"></param>
    public void GoNextStage(Task task)
    {
        task.PresentStage++;
        if (task.PresentStage >task.MaxStage)
        {
            destoryTask = task;
            Invoke("DestoryTask", 1.0f);
        }
            
    }
    /// <summary>
    /// 显示当前任务对话
    /// </summary>
    /// <param name="task"></param>
    public void ShowTaskDialog(Task task)
    {
        GameController.instance.ShowDialogPanel(Application.streamingAssetsPath+task.DialogPath+"\\Stage"+task.PresentStage+".json");
    }
    /// <summary>
    /// 获取任务奖励
    /// </summary>
    /// <param name="task"></param>
    private void GetTaskRewards(Task task)
    {
        for(int i=0;i<task.Reward.Length;i++)
        {
            Item item = ItemManager.GetItemById(task.Reward[i]);
            if (item != null)
                BagManager.instance.AddItemToBag(item, task.Amount[i],true);
            else
                Debug.LogError("Get Task Rewards unsuccessfully");
        }
    }
   
    public Task FindTaskInOpenTaskById(int id)
    {
        foreach(Task task in openTask.tasks)
        {
            if (task.Id == id) return task;
        }
        return null;
    }

    public void DestoryTask()
    {
        RemoveTaskFromTasks(destoryTask);
    }

    public void SaveTaskToJson()
    {
        Utils.WriteJsonData(openTask, Path.Task);
    }
    public void LoadTaskFromJson()
    {
        string jsonData = Utils.GetJsonData(Path.Task);
        openTask = JsonUtility.FromJson<OpenTask>(jsonData);
    }
}
