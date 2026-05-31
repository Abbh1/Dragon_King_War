using UnityEngine;

public class SkillController : MonoBehaviour
{
    public static SkillController instance;

    public GameObject fireCircle;
    public GameObject cureCircle;
    public GameObject lightingExplode;
    public GameObject fireStorm;
    private void Start()
    {
        instance = this;
    }
    public void CreateFireCircle(Vector3 position,string victimTag,State attackerState,float harmSpan,float baseHarm,float lastingTime)
    {
        GameObject obj = Instantiate(fireCircle,position,Quaternion.identity);
        obj.GetComponent<FireCircle>().Initialize(victimTag, attackerState, harmSpan, baseHarm, lastingTime);
    }
    public void CreateCureCircle(Vector3 position, string receiverTag,float cureSpan, float baseCure,float extraCure,float lastingTime)
    {
        GameObject obj = Instantiate(cureCircle, position, Quaternion.identity);
        obj.GetComponent<CureCircle>().Initialize(receiverTag, baseCure, extraCure, lastingTime, cureSpan);
    }
    public void CreateLightingExplode(Vector3 position, string victimTag, State attackerState, float baseHarm, float lastingTime)
    {
        GameObject obj = Instantiate(lightingExplode, position, Quaternion.identity);
        obj.GetComponent<LightingExplode>().Initialize(victimTag, attackerState, baseHarm, lastingTime);
    }
    public void CreateFireStorm(Vector3 position,Vector3 moveDir, float moveSpeed, float attackSpan, float lastingTime, float baseharm, string victimTag, State attackerState)
    {
        GameObject obj = Instantiate(fireStorm, position, Quaternion.identity);
        obj.GetComponent<FireStorm>().Initialize(moveDir,moveSpeed,attackSpan,lastingTime,baseharm,victimTag,attackerState);
    }
    /// <summary>
    /// 根据不同装备id释放对应技能
    /// </summary>
    /// <param name="id"></param>
    public bool EquipmentSkillMatch(int id)
    {
        switch(id)
        {
            case 9:CreateLightingExplode(GameController.instance.GetPlayerPosition(), Tags.enemy, GameController.instance.GetPlayerState(), 100, 0.5f);return true;
            case 21:CreateFireCircle(GameController.instance.GetPlayerPosition()+new Vector3(0,0.5f,0), Tags.enemy, GameController.instance.GetPlayerState(), 0.5f, 60, 20);return true;
            case 12:CreateFireStorm(GameController.instance.GetPlayerPosition(), GameController.instance.player.transform.forward, 2.0f, 0.3f, 5.0f, 50, Tags.enemy, GameController.instance.GetPlayerState());return true;
            case 17:CreateCureCircle(GameController.instance.GetPlayerPosition(), Tags.player, 0.5f, 50, 0.03f, 10.0f);return true;
            default:TipController.instance.AddTip("当前装备无技能", Tip.TipType.Quick);return false;
        }
    }
    public string GetSkillImgPath(int id)
    {
        switch(id)
        {
            case 9:return "Textures/UI/SkillIcon/LightingExplode";
            case 21:return "Textures/UI/SkillIcon/FireCircle";
            case 12:return "Textures/UI/SkillIcon/FireStorm";
            case 17:return "Textures/UI/SkillIcon/CureCircle";
            default:return Path.DefaultImgPath;
        }
    }

}
