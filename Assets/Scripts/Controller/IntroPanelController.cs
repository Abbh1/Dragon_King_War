using UnityEngine;

public class IntroPanelController : MonoBehaviour
{
    public GameObject[] panels;
    private int presentIndex;
    private void Start()
    {
        panels[0].SetActive(true);
    }
    public void ShowNextPanel()
    {
        presentIndex++;
        if (presentIndex < panels.Length)
            panels[presentIndex].SetActive(true);
        else
            SceneController.instance.LoadScene(1);

    }
}
