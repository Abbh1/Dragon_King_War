using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBar : MonoBehaviour
{
    public List<Task> pickTasks;//记录当前正在进行任务
    public Task selectedTask;//被点击的任务
    public TaskSlot[] slots;
    public static TaskBar instance;
    public Text title;
    public Text description;
    public Text target;
    public Text rewards;
    private void Start()
    {
        slots = GetComponentsInChildren<TaskSlot>();
        instance = this;
    }
    private void Update()
    {
        UpdatePickTasks();
        UpdateTaskInfo();
        UPdateTaskSlots();
    }
    private void UpdatePickTasks()
    {
        pickTasks = new List<Task>();
        foreach(Task task in TaskController.instance.openTask.tasks)
        {
            if (task.PresentStage>0&&task.PresentStage<=task.MaxStage)
            {
                pickTasks.Add(task);
            }
        }
    }
    private void UpdateTaskInfo()
    {
        if(selectedTask!=null&&selectedTask.MaxStage>0)
        {
            title.text = selectedTask.Title;
            description.text = selectedTask.Description[selectedTask.PresentStage];
            target.text = selectedTask.Target[selectedTask.PresentStage];
            string rewardsText=null;
            for(int i=0;i<selectedTask.Reward.Length;i++)
            {
                rewardsText += ItemManager.GetItemById(selectedTask.Reward[i]).Name +"x"+ selectedTask.Amount[i]+"\n";
            }
            rewards.text = rewardsText;
        }
        else
        {
            title.text = "";
            description.text = "";
            target.text = "";
            rewards.text = "";
        }
    }
    private void UPdateTaskSlots()
    {
        for(int i=0;i<slots.Length;i++)
        {
            if (i < pickTasks.Count)
                slots[i].task = pickTasks[i];
            else
                slots[i].task = null;
        }
    }
}
