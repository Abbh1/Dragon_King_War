using System.Collections.Generic;
using UnityEngine;
public class Buff
{
    public float startTime;
    public float lastingTime;
    public float attack;
    public float defence;
    public float harmIncrease;
    public float harmReduction;
    public float critChance;
    public float critRate;
    public float absorbRate;
    public float maxHp;
    public float maxMp;

    public Buff(float lastingTime, float attack, float defence, float harmIncrease, float harmReduction, float critChance, float critRate, float absorbRate, float maxHp, float maxMp)
    {
        this.lastingTime = lastingTime;
        this.attack = attack;
        this.defence = defence;
        this.harmIncrease = harmIncrease;
        this.harmReduction = harmReduction;
        this.critChance = critChance;
        this.critRate = critRate;
        this.absorbRate = absorbRate;
        this.maxHp = maxHp;
        this.maxMp = maxMp;
    }
}
public class BuffController : MonoBehaviour
{
    public float tempAttack;
    public float tempDefence;
    public float tempHarmIncrease;
    public float tempHarmReduction;
    public float tempCritChance;
    public float tempCritRate;
    public float tempAbsorbRate;
    public float tempMaxHp;
    public float tempMaxMp;
    public static BuffController instance;
    private float timer;
    public List<Buff> buffs;
    private void Start()
    {
        instance = this;
        buffs = new List<Buff>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        BuffManage();
    }

    private void BuffManage()
    {
        tempAttack = 0;
        tempDefence = 0;
        tempHarmIncrease = 0;
        tempHarmReduction = 0;
        tempCritChance = 0;
        tempCritRate = 0;
        tempAbsorbRate = 0;
        tempMaxHp = 0;
        tempMaxMp = 0;
        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            tempAttack += buffs[i].attack;
            tempDefence += buffs[i].defence;
            tempHarmIncrease += buffs[i].harmIncrease;
            tempHarmReduction += buffs[i].harmReduction;
            tempCritChance += buffs[i].critChance;
            tempCritRate += buffs[i].critRate;
            tempAbsorbRate += buffs[i].absorbRate;
            tempMaxHp += buffs[i].maxHp;
            tempMaxMp += buffs[i].maxMp;
            if (timer - buffs[i].startTime > buffs[i].lastingTime)
                buffs.RemoveAt(i);
        }
    }
    

    public void AddBuff(Buff buff)
    {
        buff.startTime = timer;
        buffs.Add(buff);
    }
    
}
