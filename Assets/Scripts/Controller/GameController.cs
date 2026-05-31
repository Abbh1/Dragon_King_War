using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject player;
    public ItemDetect itemDetect;

    public Image weaponSkillMask;
    public Image weaponSkillImg;
    public Text weaponSkillLeftTime;
    public Image armorSkillMask;
    public Image armorSkillImg;
    public Text armorSkillLeftTime;
    public Image attackMask;
    public Text attackLeftTime;
    public Image blockMask;
    public Text blockLeftTime;
    public Slider hpBar;
    public Text hpText;
    public Slider mpBar;
    public Text mpText;
    public Slider expBar;
    public Text expText;

    public GameObject common;
    public GameObject uncommon;
    public GameObject rare;
    public GameObject epic;
    public GameObject legendary;
    public GameObject artifact;

    public GameObject bagPanel;
    public GameObject pickPanel;
    public GameObject shopPanel;
    public GameObject dialogPanel;
    public GameObject taskPanel;
    public GameObject forgePanel;
    public GameObject notifyPanel;
    public GameObject gamblingPanel;

    public Vector3 waterFountainPos;

    public bool shopPanelActive;
    public bool forgePanelActive;
    public bool gamblingPanelActive;
    float shakeTime = 1.0f;//震动时间
    private float currentTime = 0.0f;
    private List<Vector3> gameobjpons = new List<Vector3>();
    public Camera shakeCamera;//要求震动的相机

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    private void Update()
    {  
        //更新状态
        player = GameObject.Find("Player");
        UpdateMpBar();
        UpdateHpBar();
        UpdateExpBar();
        UpdateSkillCD();
        //打开背包
        if(Utils.GetKeyDown(KeyCode.B,SettingManager.instance.inputInfo.BagKey))
        {
            SetGameObjectActive(bagPanel);
            Invoke("SetBagModeUse", 0.1f);
        }
        //打开任务
        if(Utils.GetKeyDown(KeyCode.V,SettingManager.instance.inputInfo.TaskKey))
        {
            SetGameObjectActive(taskPanel);
        }
        //拾取界面
        if (itemDetect.nearItemObjs.Count > 0)
            SetGameObjectActive(pickPanel);
        else
            SetGameObjectInactive(pickPanel);
        //F键交互
        if(Utils.GetKeyDown(KeyCode.F,SettingManager.instance.inputInfo.InterActKey))
        {
            if(shopPanelActive)
                SetGameObjectActive(shopPanel);
            if (forgePanelActive)
                SetGameObjectActive(forgePanel);
            if (gamblingPanelActive)
                SetGameObjectActive(gamblingPanel);
        }
    }

    void LateUpdate() { UpdateShake(); }
    /// <summary>
    /// 返回当前玩家位置
    /// </summary>
    /// <returns></returns>
    public  Vector3 GetPlayerPosition()
    {
        if (player == null)
            return Vector3.zero;
        else
            return player.transform.position;
    }
    public Transform GetPlayerTransform()
    {
        return player.transform;
    }
    public State GetPlayerState()
    {
        return player.GetComponent<State>();
    }
    private void UpdateShake()
    {
        if (currentTime > 0.0f)
        {
            currentTime -= Time.deltaTime;
            shakeCamera.rect = new Rect(0.04f * (-1.0f + 2.0f * Random.value) * Mathf.Pow(currentTime, 2), 0.04f * (-1.0f + 2.0f * Random.value) * Mathf.Pow(currentTime, 2), 1.0f, 1.0f);
        }
        else
        {
            currentTime = 0.0f;
        }
    }
    public void ShakeCamera()
    {
        currentTime = shakeTime; 
    }
    public void UpdateHpBar()
    {
        hpBar.value = GetPlayerState().value.presentHP / GetPlayerState().value.maxHP;
        hpText.text = (int)GetPlayerState().value.presentHP+" / "+ (int)GetPlayerState().value.maxHP;
    }
    public void UpdateMpBar()
    {
        mpBar.value = GetPlayerState().value.presentMP / GetPlayerState().value.maxMP;
        mpText.text = (int)GetPlayerState().value.presentMP + " / " + (int)GetPlayerState().value.maxMP;
    }
    public void UpdateExpBar()
    {
        int[] info= Utils.GetLevelByExp(player.GetComponent<Player>().exp);
        int level = info[0];
        int exp = info[1];
        GetPlayerState().value.level = level;
        expBar.value = exp / Utils.GetExpByLevel(level+1);
        expText.text = exp + "/" + Utils.GetExpByLevel(level+1);
    }
    public void UpdateSkillCD()
    {
        //普通攻击
        attackMask.fillAmount = player.GetComponent<Player>().leftAttackTime / GetPlayerState().value.attackSpan;
        if (player.GetComponent<Player>().leftAttackTime != 0)
            attackLeftTime.text = player.GetComponent<Player>().leftAttackTime.ToString("0.0") + "S";
        else
            attackLeftTime.text = " ";
        //格挡
        blockMask.fillAmount = player.GetComponent<Player>().leftBlockTime / player.GetComponent<Player>().blockSpan;
        if (player.GetComponent<Player>().leftBlockTime != 0)
            blockLeftTime.text = player.GetComponent<Player>().leftBlockTime.ToString("0.0") + "S";
        else
            blockLeftTime.text = " ";

        //武器技能
        weaponSkillImg.sprite = Utils.LoadSprite(SkillController.instance.GetSkillImgPath(BagManager.instance.weaponSlot.item.Id));
        weaponSkillMask.fillAmount = player.GetComponent<Player>().leftWeaponSkillTime / player.GetComponent<Player>().weaponSkillCD;
        if (player.GetComponent<Player>().leftWeaponSkillTime != 0)
            weaponSkillLeftTime.text = player.GetComponent<Player>().leftWeaponSkillTime.ToString("0.0") + "S";
        else
            weaponSkillLeftTime.text = " ";
        //防具技能
        armorSkillImg.sprite = Utils.LoadSprite(SkillController.instance.GetSkillImgPath(BagManager.instance.armorSlot.item.Id));
        armorSkillMask.fillAmount  = player.GetComponent<Player>().leftArmorSkillTime / player.GetComponent<Player>().armorSkillCD;
        if (player.GetComponent<Player>().leftArmorSkillTime != 0)
            armorSkillLeftTime.text = player.GetComponent<Player>().leftArmorSkillTime.ToString("0.0") + "S";
        else
            armorSkillLeftTime.text = " ";
    }
    public void CreateItems(Item item,Vector3 position)
    {
        GameObject obj=null;
        switch (item.Quality)
        {
            case Item.ItemQuality.Common:
                 obj = Instantiate(common, position, Quaternion.identity);
                break;
            case Item.ItemQuality.Uncommon:
                 obj = Instantiate(uncommon, position, Quaternion.identity);
                break;
            case Item.ItemQuality.Rare:
                obj = Instantiate(rare, position, Quaternion.identity);
                break;
            case Item.ItemQuality.Epic:
                obj = Instantiate(epic, position, Quaternion.identity);
                break;
            case Item.ItemQuality.Legendary:
                obj = Instantiate(legendary, position, Quaternion.identity);
                break;
            case Item.ItemQuality.Artifact:
                obj = Instantiate(artifact, position, Quaternion.identity);
                break;
            default:
                break;
        }
        obj.GetComponent<ItemObject>().item = item;
    }
    public void SetGameObjectActive(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void SetGameObjectInactive(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void ShowDialogPanel(string path)
    {
        player.GetComponent<Player>().state.canMove = false;
        dialogPanel.GetComponent<DialogController>().LoadDialogInfo(path);
        dialogPanel.SetActive(true);
    }
    private void SetBagModeUse()
    {
        BagSlotsController.instance.mode = BagSlotsController.Mode.Use;
    }
    public void SaveAll()
    {
        player.GetComponent<Player>().SavePlayerInfoToJson();
        BagManager.instance.SaveBagToJson();
        BagManager.instance.SaveEquipmentToJson();
        BagManager.instance.SaveMoneyToJson();
        TaskController.instance.SaveTaskToJson();
    }
}
