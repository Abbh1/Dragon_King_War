using UnityEngine;
using UnityEngine.UI;

public class TaskSlot : MonoBehaviour
{
    public Task task;
    public Text title;
    private Image img;
    private void Start()
    {
        img = GetComponent<Image>();
    }
    private void Update()
    {
        if (task != null)
        {
            title.text = task.Title;
            img.sprite = Utils.LoadSprite("Textures/UI/TaskPanel/TaskSlot");
        }
        else
        {
            img.sprite = Utils.LoadSprite(Path.DefaultImgPath);
            title.text = "";
        }
    }
    public void OnSelected()
    {
        if (task != null)
            TaskBar.instance.selectedTask = task;
        else
            TaskBar.instance.selectedTask = null;
    }
}
