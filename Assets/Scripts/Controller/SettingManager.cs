using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InputInfo
{
    public KeyCode RollKey;
    public KeyCode WeaponSkillKey;
    public KeyCode ArmorSkillKey;
    public KeyCode AttackKey;
    public KeyCode BlockKey;
    public KeyCode InterActKey;
    public KeyCode BagKey;
    public KeyCode TaskKey;
}
public class SettingManager : MonoBehaviour
{
    public InputInfo inputInfo;
    public static SettingManager instance;
    public Text attackText;
    public Text blockText;
    public Text weaponSkillText;
    public Text armorSkillText;
    public Text rollText;
    public Text interactText;
    public Text bagText;
    public Text taskText;
    public Slider mainVolume;
    public Slider backgroundVolume;
    private bool isListening;
    private int keyIndex;
    private Text selectedText;
    void Start()
    {
        instance = this;
        ResetInputInfo();
    }
    private void Update()
    {
        AudioListener.volume = mainVolume.value;
        MusicController.instance.SetVolume(backgroundVolume.value);
    }
    private void OnGUI()
    {
        if (isListening) GetCurrentKey();
    }
    private void GetCurrentKey()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    isListening = false;
                    selectedText.text = keyCode.ToString();
                    switch(keyIndex)
                    {
                        case 1:inputInfo.AttackKey = keyCode;break;
                        case 2:inputInfo.BlockKey = keyCode;break;
                        case 3:inputInfo.RollKey = keyCode;break;
                        case 4:inputInfo.WeaponSkillKey = keyCode;break;
                        case 5:inputInfo.ArmorSkillKey = keyCode;break;
                        case 6:inputInfo.InterActKey = keyCode;break;
                        case 7:inputInfo.BagKey = keyCode; break;
                        case 8:inputInfo.TaskKey = keyCode;break;
                        default:break;
                    }
                    break;
                }
            }
        }
    }
    private void SaveInputInfoToJson()
    {
        Utils.WriteJsonData(inputInfo, Path.InputInfoPath);
    }
    private void LoadInputInfoFromJson(string path)
    {
        string jsonData = Utils.GetJsonData(path);
        inputInfo = JsonUtility.FromJson<InputInfo>(jsonData);
    }
    public void CloseSettingPanel()
    {
        isListening = false;
        SaveInputInfoToJson();
        SceneController.instance.settingCanva.SetActive(false);
    }
    public void SetSelectedText(int i)
    {
        keyIndex = i;
        isListening = true;
        switch(i)
        {
            case 1:selectedText = attackText;break;
            case 2:selectedText = blockText;break;
            case 3:selectedText = rollText;break;
            case 4:selectedText = weaponSkillText;break;
            case 5:selectedText = armorSkillText;break;
            case 6:selectedText = interactText;break;
            case 7:selectedText = bagText;break;
            case 8:selectedText = taskText;break;
            default:selectedText = null;break;
        }
    }
    private void SetAllText()
    {
        attackText.text = inputInfo.AttackKey.ToString();
        blockText.text = inputInfo.BlockKey.ToString();
        rollText.text = inputInfo.RollKey.ToString();
        weaponSkillText.text = inputInfo.WeaponSkillKey.ToString();
        armorSkillText.text = inputInfo.ArmorSkillKey.ToString();
        interactText.text = inputInfo.InterActKey.ToString();
        bagText.text = inputInfo.BagKey.ToString();
        taskText.text = inputInfo.TaskKey.ToString();
    }
    public void ResetInputInfo()
    {
        LoadInputInfoFromJson(Path.DefaultInputInfoPath);
        SetAllText();
    }
    public void LoadInputInfo()
    {
        LoadInputInfoFromJson(Path.InputInfoPath);
        SetAllText();
    }
}
