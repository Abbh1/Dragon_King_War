/// <summary>
/// 对话类
/// </summary>
[System.Serializable]
public class Dialog 
{
    public string lImgPath;//左立绘存储路径
    public string rImgPath;//右立绘存储路径
    public string titie;//对话标题
    public string message;//对话内容
    public Speaker speaker;//当前对话说话者
    public enum Speaker
    {
        left,
        right
    }
}
