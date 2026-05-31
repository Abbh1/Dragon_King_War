using UnityEngine;

[System.Serializable]
public class TaskInfomation
{
    public Task[] tasks;
}
public class TaskManager
{
    public static TaskInfomation GetTaskInfomation()
    {
        string jsonData = Utils.GetJsonData(Path.TaskInfoPath);
        TaskInfomation info = JsonUtility.FromJson<TaskInfomation>(jsonData);
        return info;
    }
    public static Task GetTaskById(int id)
    {
        TaskInfomation info = GetTaskInfomation();
        foreach (Task task in info.tasks)
        {
            if (task.Id == id)
            {
                return task;
            }
        }
        Debug.Log("no such id task");
        return null;
    }
}
