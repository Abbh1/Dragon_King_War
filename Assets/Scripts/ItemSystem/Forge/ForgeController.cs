using UnityEngine;
public class ForgeController : MonoBehaviour
{
    public static ForgeController instance;
    public OriginMaterialSlot selectedSlot;
    private bool canForge;
    private int[] materialIds;
    private int[] productIds;
    private OriginMaterialSlot[] materialSlots;
    private ProductSlot[] productSlots;
    
    private void Start()
    {
        instance = this;
        materialSlots = GetComponentsInChildren<OriginMaterialSlot>();
        productSlots = GetComponentsInChildren<ProductSlot>();
    }
    private void Update()
    {
        int length = 0;
        foreach(OriginMaterialSlot slot in materialSlots)
        {
            if (slot.item.Id != 0)
                length++;
        }
        materialIds = new int[length];
        int i = 0;
        foreach(OriginMaterialSlot slot in materialSlots)
        {
            if(slot.item.Id!=0)
            {
                materialIds[i] = slot.item.Id;
                i++;
            }
        }
        productIds = ForgeManager.instance.GetProductIdsByMatrialIds(materialIds);
        if(productIds!=null)
        {
            canForge = true;
            for(int j=0;j<productIds.Length;j++)
            {
                productSlots[j].item = ItemManager.GetItemById(productIds[j]);
            }
        }
        else
        {
            canForge = false;
            foreach (ProductSlot productSlot in productSlots)
            {
                productSlot.item = new Item();
            }

        }
    }
    public void OnClicked()
    {
        if(canForge)
        {
            foreach (ProductSlot productSlot in productSlots)
            {
                if (productSlot.item.Id != 0)
                {
                    BagManager.instance.AddItemToBag(ItemManager.GetItemById(productSlot.item.Id), 1, true);
                    productSlot.item = new Item();
                }
            }
            foreach (OriginMaterialSlot originMaterialSlot in materialSlots)
            {
                originMaterialSlot.item = new Item();
            }
        }
        else
        {
            TipController.instance.AddTip("无对应配方", Tip.TipType.Quick);
        }
    }
    public void Close()
    {
        foreach (OriginMaterialSlot originMaterialSlot in materialSlots)
        {
            if (originMaterialSlot.item.Id != 0)
            {
                BagManager.instance.AddItemToBag(ItemManager.GetItemById(originMaterialSlot.item.Id), 1, false);
                originMaterialSlot.item = new Item();
            }
        }
        foreach (ProductSlot productSlot in productSlots)
        {
            productSlot.item = new Item();
        }
            gameObject.SetActive(false);
    }
}
