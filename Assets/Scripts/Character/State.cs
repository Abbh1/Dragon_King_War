using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Value
{
    public string name;
    public int level;
    public float baseHP;
    public float baseMP;
    public float baseAttack;
    public float baseDefence;
    public float baseCritChance;
    public float baseCritRate;
    public float maxHP;
    public float presentHP;
    public float maxMP;
    public float presentMP;
    public float attack;
    public float defence;
    public float critChance;
    public float critRate;
    public float attackSpan;
    public float runSpeed;
    public float walkSpeed;
    public float rotateSpeed;
    public float damageReduction;
    public float damageIncrease;
    public float attackDistance;
    public float alarmDistance;
}
public class State : MonoBehaviour
{
    public bool isAttacked;
    public bool canMove=true;
    public Value value;
    public float harm;
    public void LoadValue(string path)
    {
        string jsonData = Utils.GetJsonData(path);
        value = JsonUtility.FromJson<Value>(jsonData);
    }
}
