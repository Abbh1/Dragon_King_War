using UnityEngine;
/// <summary>
/// 存储对话信息
/// </summary>
[System.Serializable]
public class DialogInfomation
{
    public Dialog[] dialogs;
}

public class DialogManager
{
    public static DialogInfomation GetDialogInformation(string path)
    {
        string jsonData = Utils.GetJsonData(path);
        DialogInfomation info = JsonUtility.FromJson<DialogInfomation>(jsonData);
        return info;
    }

}
