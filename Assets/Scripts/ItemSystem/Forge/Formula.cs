[System.Serializable]
public class Formula
{
    public int[] OrignMaterials;//原材料物品ID
    public int[] Products;//产品ID

    public Formula(int[] orignMaterials, int[] products)
    {
        OrignMaterials = orignMaterials;
        Products = products;
    }
}
