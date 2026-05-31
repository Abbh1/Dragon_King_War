using System;
using UnityEngine;
/// <summary>
/// 存储配方信息
/// </summary>
[System.Serializable]
public class FormulaInfo
{
    public Formula[] formulas;
}
public class ForgeManager : MonoBehaviour
{
    public FormulaInfo formulaInfo;
    public static ForgeManager instance;
    private void Start()
    {
        instance = this;
        LoadFormulaInfo();
    }
    private void LoadFormulaInfo()
    {
        string jsonData = Utils.GetJsonData(Path.FormulaInfoPath);
        formulaInfo= JsonUtility.FromJson<FormulaInfo>(jsonData);
    }
    /// <summary>
    /// 查询是否有匹配配方并返回产物ID
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public int[] GetProductIdsByMatrialIds(int[] ids)
    {
        foreach(Formula formula in formulaInfo.formulas)
        {
            if (formula.OrignMaterials.Length==ids.Length)
            {
                bool match = true;
                Array.Sort(formula.OrignMaterials);
                Array.Sort(ids);
                for(int i=0;i<ids.Length;i++)
                {
                    if (ids[i] != formula.OrignMaterials[i])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    return formula.Products;
                }
            }
        }
        return null;
    }
}
