using UnityEngine;
[System.Serializable]
public class PlayerInfo
{
    public int exp;
    public Vector3 pos;

    public PlayerInfo(int exp, Vector3 pos)
    {
        this.exp = exp;
        this.pos = pos;
    }
}
public class Player : MonoBehaviour
{
    private Transform camTransform;
    public bool isRun;
    public bool isAttack;
    private Vector3 camForward; //临时三维坐标
    public State state;
    public int exp;
    public float weaponSkillCD;
    private float lastWeaponSkillTime=-100.0f;
    public float leftWeaponSkillTime;
    public float armorSkillCD;
    private float lastArmorSkillTime=-100.0f;
    public float leftArmorSkillTime;
    private float lastAttackTime = -100.0f;
    public float leftAttackTime;
    public float blockSpan = 5.0f;
    private float lastBlockTime=-100.0f;
    public float leftBlockTime;
    public float mpConsume = 10.0f;
    public GameObject normalAttack;
    public GameObject blockAttack;
    public AudioClip[] clips;
    private CharacterController cc;
    private Animator anim;
    private float timer;
    private AudioSource baseAudio;//控制攻击以及翻滚
    private AudioSource runAudio;//控制移动
    void Start()
    {
        camTransform = Camera.main.transform;
        state = GetComponent<State>();
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        baseAudio = GetComponents<AudioSource>()[0];
        runAudio = GetComponents<AudioSource>()[1];
        if (SceneController.instance != null && !SceneController.instance.isNewGame)
            LoadPlayerInfo();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SceneController.instance.LoadScene(2);
        }
        UpdateValue();
        timer += Time.deltaTime;
        #region 更新技能CD
        leftWeaponSkillTime = (weaponSkillCD - timer + lastWeaponSkillTime<0)?0: weaponSkillCD - timer + lastWeaponSkillTime;
        leftArmorSkillTime = (armorSkillCD - timer + lastArmorSkillTime < 0) ? 0 : armorSkillCD - timer + lastArmorSkillTime;
        leftAttackTime = (state.value.attackSpan - timer + lastAttackTime < 0) ? 0 : state.value.attackSpan - timer + lastAttackTime;
        leftBlockTime = (blockSpan - timer + lastBlockTime < 0) ? 0 : blockSpan - timer + lastBlockTime;
        #endregion
        #region 连招攻击
        if (Utils.GetKeyDown(KeyCode.J,SettingManager.instance.inputInfo.AttackKey))
        {
            if ((anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")||anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))&&timer-lastAttackTime>state.value.attackSpan)
            {
                anim.SetTrigger("attack1");
                lastAttackTime = timer;
            }
            if (combo == 1)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
                {
                    anim.SetTrigger("attack2");
                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
                {
                    anim.SetTrigger("attack3");
                }
                anim.SetBool("attackFinish", false);
                lastAttackTime = timer;
            }
        }
        if(combo==0)
        {
            anim.SetBool("attackFinish", true);
        }
        #endregion
        #region 翻滚
        if (Utils.GetKeyDown(KeyCode.LeftShift,SettingManager.instance.inputInfo.RollKey)&& anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            anim.SetTrigger("roll");
            baseAudio.clip = clips[3];
            baseAudio.Play();
        }
        if (isRoll == 1)
        {
          //翻滚处理
        }
        #endregion
        #region 格挡
        if (Utils.GetKeyDown(KeyCode.H,SettingManager.instance.inputInfo.BlockKey) && (anim.GetCurrentAnimatorStateInfo(0).IsName("Run") || anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Roll")))
        {
            if(timer - lastBlockTime > blockSpan)
            {
                lastBlockTime = timer;
                anim.SetTrigger("block");
                anim.SetBool("blockIdle", true);
            }
            else
                TipController.instance.AddTip("格挡技能冷却中", Tip.TipType.Quick);
        }
        if((Input.GetKeyUp(KeyCode.H)||SettingManager.instance==null)&&Input.GetKeyUp(SettingManager.instance.inputInfo.BlockKey))
        {
            anim.SetBool("blockIdle", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Block") && state.isAttacked)
        {
            lastBlockTime = -100.0f;
            state.isAttacked = false;
            state.harm = 0;
            blockAttack.GetComponent<BlockAttack>().isActive = true;
            baseAudio.clip = clips[4];
            baseAudio.Play();
            Debug.Log("success");
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("BlockIdle")&& state.isAttacked)
        {
            state.isAttacked = false;
            state.harm = 0;
        }
        #endregion
        #region 受击
        if(state.isAttacked)
        {
            baseAudio.clip = clips[5];
            baseAudio.Play();
            Attacked();
        }
        #endregion
        #region 武器技能
        if(Utils.GetKeyDown(KeyCode.E,SettingManager.instance.inputInfo.WeaponSkillKey)&& BagManager.instance != null)
        {
            if (timer - lastWeaponSkillTime > weaponSkillCD)
            {
                if (state.value.presentMP >= mpConsume)
                {
                    if (SkillController.instance.EquipmentSkillMatch(BagManager.instance.weaponSlot.item.Id))
                    {
                        state.value.presentMP -= mpConsume;
                        lastWeaponSkillTime = timer;
                    }
                }
                else
                    TipController.instance.AddTip("MP剩余不足", Tip.TipType.Middle);
            }
            else
                TipController.instance.AddTip("武器技能冷却中", Tip.TipType.Middle);
        }
        #endregion
        #region 防具技能
        if(Utils.GetKeyDown(KeyCode.R,SettingManager.instance.inputInfo.ArmorSkillKey)&& BagManager.instance != null)
        {
            if (timer - lastArmorSkillTime > armorSkillCD)
            {
                if(state.value.presentMP>=mpConsume)
                {
                    if (SkillController.instance.EquipmentSkillMatch(BagManager.instance.armorSlot.item.Id))
                    {
                        state.value.presentMP -= mpConsume;
                        lastArmorSkillTime = timer;
                    }
                }
                else
                {
                    TipController.instance.AddTip("MP剩余不足", Tip.TipType.Middle);
                }
            }
            else
                TipController.instance.AddTip("防具技能冷却中", Tip.TipType.Middle);
        }
        #endregion
        #region 使用药品
        if (Input.GetKeyDown(KeyCode.T))
        {

        }
        #endregion
        #region 重生检测
        if (state.value.presentHP <= 0)
            Reborn();
        #endregion

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Run();
    }
    public void TransmitToPos(Vector3 pos)
    {
        if(SceneController.instance!=null)
            SceneController.instance.ShowLoadingCanva();
        cc.enabled = false;
        transform.position = pos;
        cc.enabled = true;
    }
    private void Reborn()
    {
        TransmitToPos(GameController.instance.waterFountainPos);
        state.value.presentHP = state.value.maxHP;
        state.value.presentMP = state.value.maxMP;
    }
    #region 移动控制函数
    private void Run()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if ((Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)&&state.canMove)
        {
            anim.SetBool("run", true);
            isRun = true;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run") || anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                if (!runAudio.isPlaying)
                    runAudio.Play();
                cc.Move(camTransform.right * h * state.value.runSpeed * Time.deltaTime + camForward * v * state.value.runSpeed * Time.deltaTime);
                //水平垂直方向系数不为0表示需要进行旋转
                if (h != 0 || v != 0)
                {
                    Rotating(h, v);
                }
            }
        }
        else
        {
            anim.SetBool("run", false);
            isRun = false;
            runAudio.Pause();
        }
    }
    void Rotating(float hh, float vv)
    {
        camForward = Vector3.Cross(camTransform.right, Vector3.up);
        Vector3 targetDir = camTransform.right * hh + camForward * vv;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, state.value.rotateSpeed * Time.deltaTime);
    }
    #endregion
    #region 动画事件函数
    private int isRoll;//为1时表示当前处于翻滚
    private void SetRollState(int state)
    {
        isRoll = state;
    }
    
    public int combo;//为1时可以进行连击
    void SetComboState(int state)
    {
        combo = state;
    }

    #endregion
    #region 攻击事件函数
    void Attack1Start()
    {
        normalAttack.GetComponent<NormalAttack>().harm = 1;
        normalAttack.SetActive(true);
        baseAudio.clip = clips[0];
        baseAudio.Play();
        Debug.Log("attack1");
    }
    void Attack2Start()
    {
        normalAttack.GetComponent<NormalAttack>().harm = 2;
        normalAttack.SetActive(true);
        baseAudio.clip = clips[1];
        baseAudio.Play();
        Debug.Log("attack2");
    }

    void Attack3Start()
    {
        normalAttack.GetComponent<NormalAttack>().harm = 3;
        normalAttack.SetActive(true);
        baseAudio.clip = clips[2];
        baseAudio.Play();
        Debug.Log("attack3");
    }
    void AttackEnd()
    {
        normalAttack.SetActive(false);
        isAttack = false;
    }

    void Attacked()
    {
        state.isAttacked = false;
        state.value.presentHP -= state.harm;
        state.harm = 0;
        Debug.Log("Player is Attacked");
        anim.SetTrigger("attacked");
        GameController.instance.ShakeCamera();
    }
    #endregion
    #region 数值控制函数
    private void UpdateValue()
    {
        Value value = state.value;
        Utils.SetBaseValueByLevel(value);
        if (BagManager.instance != null)
        {
            Item weapon = (BagManager.instance.weaponSlot.item != null) ? BagManager.instance.weaponSlot.item : new Item();
            Item armor = (BagManager.instance.armorSlot.item != null) ? BagManager.instance.armorSlot.item : new Item();
            Item consumble = (BagManager.instance.consumbleSlot.item != null) ? BagManager.instance.consumbleSlot.item : new Item();
            value.attack = value.baseAttack + weapon.Attack + armor.Attack + consumble.Attack + BuffController.instance.tempAttack;
            value.defence = value.baseDefence + weapon.Defense + armor.Defense + consumble.Defense + BuffController.instance.tempDefence;
            value.critChance = value.baseCritChance + weapon.CritChance + armor.CritChance + consumble.CritChance + BuffController.instance.tempCritChance;
            value.critRate = value.baseCritRate + weapon.CritRate + armor.CritRate + consumble.CritRate + BuffController.instance.tempCritRate;
        }
        else
        {
            value.attack = value.baseAttack + BuffController.instance.tempAttack;
            value.defence = value.baseDefence + BuffController.instance.tempDefence;
            value.critChance = value.baseCritChance + BuffController.instance.tempCritRate;
            value.critRate = value.baseCritRate + BuffController.instance.tempCritRate;
        }
        value.maxHP = value.baseHP + BuffController.instance.tempMaxHp;
        value.maxMP = value.baseMP + BuffController.instance.tempMaxMp;
        value.damageIncrease = BuffController.instance.tempHarmIncrease;
        value.damageReduction = BuffController.instance.tempHarmReduction;
        value.presentHP = (value.presentHP > value.maxHP) ? value.maxHP : value.presentHP;
        value.presentHP = (value.presentHP < 0) ? 0 : value.presentHP;
        value.presentMP = (value.presentMP > value.maxMP) ? value.maxMP : value.presentMP;
        value.presentMP = (value.presentMP < 0) ? 0 : value.presentMP;
    }
    #endregion
    #region 存档控制函数
    public void SavePlayerInfoToJson()
    {
        PlayerInfo playerInfo = new PlayerInfo(exp, transform.position);
        Utils.WriteJsonData(playerInfo, Path.PlayerInfoPath);
    }
    public void LoadPlayerInfo()
    {
        string jsonData = Utils.GetJsonData(Path.PlayerInfoPath);
        PlayerInfo playerInfo= JsonUtility.FromJson<PlayerInfo>(jsonData);
        exp = playerInfo.exp;
        TransmitToPos(playerInfo.pos);
    }
    #endregion
}
