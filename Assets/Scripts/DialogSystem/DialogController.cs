using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public Image lImg;
    public Image rImg;
    public Text title;
    public Text message;
    private DialogInfomation info;
    private int index;//记录当前从info中读取dialog下标

    public void LoadDialogInfo(string infoPath)
    {
        index = 0;
        info = DialogManager.GetDialogInformation(infoPath);
        ShowDialog(info.dialogs[0]);
    }

    private void ShowDialog(Dialog dialog)
    {
        if (dialog.speaker == Dialog.Speaker.right)
        {
            rImg.sprite = Utils.LoadSprite(dialog.rImgPath);
            lImg.sprite = Utils.LoadSprite(Path.DefaultImgPath);
        }
        else
        {
            rImg.sprite = Utils.LoadSprite(Path.DefaultImgPath);
            lImg.sprite = Utils.LoadSprite(dialog.lImgPath);
        }
        title.text = dialog.titie;
        message.text = dialog.message;
    }
    
    public void UpdateDiaLog()
    {
        index++;
        if(index<info.dialogs.Length)
        {
            ShowDialog(info.dialogs[index]);
        }
        else
        {
            //结束处理
            gameObject.SetActive(false);
            GameController.instance.GetPlayerState().canMove = true;
        }
    }
}
