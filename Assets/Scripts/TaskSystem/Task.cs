[System.Serializable]
public class Task 
{
    public int Id;//大于0时合法
    public TaskType Type;
    public string Title;
    public int MaxStage;//任务阶段数
    public int PresentStage;//0表示未接取MaxStage+1时表示任务完成
    public string DialogPath;//任务对话json文件根目录, 此目录下应有maxstage个json文件命名规范为Stage+number
    public int[] NextTask;//完成该任务后解锁后续任务id号
    public string[] Description;//当前阶段任务栏描述
    public string[] Target;//当前阶段任务栏目标
    public int[] Reward;//完成任务后奖励物品ID
    public int[] Amount;//完成任务后奖励物品对应数目

    public enum TaskType
    {
        Main,
        Side,
        Daily
    }
}
