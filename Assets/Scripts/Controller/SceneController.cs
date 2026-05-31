using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public bool isNewGame=true;
    public GameObject introPanel;
    public GameObject loadingCanva;
    public GameObject settingCanva;
    public Image img;
    private AsyncOperation asyncOperation;
    public Slider processBar;
    public static SceneController instance;
    private bool isShow;
    private bool haveEntered;
    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            settingCanva.SetActive(true);
        if (asyncOperation != null&&!haveEntered)
        {
            processBar.value = asyncOperation.progress;
            if(asyncOperation.isDone)
            {
                MusicController.instance.PlayNormalMusic();
                processBar.value = 0;
                loadingCanva.SetActive(false);
                haveEntered = true;
            }
        }  
        if(isShow)
        {
            processBar.value += Time.deltaTime;
            if(processBar.value>=1)
            {
                processBar.value = 0;
                loadingCanva.SetActive(false);
                isShow = false;
            }
            
        }
    }
    /// <summary>
    /// «–ªª≥°æ∞
    /// </summary>
    /// <param name="index">≥°æ∞–Ú∫≈</param>
    public void LoadScene(int index)
    {
        haveEntered = false;
        int i = Utils.GetRandomIntInRange(1, 4);
        img.sprite= Utils.LoadSprite("Textures/UI/LoadingPanel/Loading"+i.ToString());
        loadingCanva.SetActive(true);
        DontDestroyOnLoad(gameObject);
        asyncOperation = SceneManager.LoadSceneAsync(index);
    }
    /// <summary>
    /// œ‘ æº”‘ÿ“≥√Ê
    /// </summary>
    public void ShowLoadingCanva()
    {
        loadingCanva.SetActive(true);
        isShow = true;
    }
    public void ShowSettingPanel()
    {
        settingCanva.SetActive(true);
    }
    public void SaveGame()
    {
        if(GameController.instance!=null)
            GameController.instance.SaveAll();
    }

    public void StartNewGame()
    {
        if (introPanel != null)
            introPanel.SetActive(true);
        MusicController.instance.PlayIntroMusic();
        isNewGame = true;
    }
    public void ContinueGame()
    {
        isNewGame = false;
        settingCanva.SetActive(false);
        LoadScene(1);
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
