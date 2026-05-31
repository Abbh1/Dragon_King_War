using UnityEngine;
using UnityEngine.UI;

public class ProductSlot : MonoBehaviour
{
    public Item item;
    private Image img;
    private void Start()
    {
        img = GetComponent<Image>();
    }
    private void Update()
    {
        if (item.Id == 0)
        {
            img.sprite = Utils.LoadSprite(Path.DefaultImgPath);
        }
        else
        {
            img.sprite = Utils.LoadSprite(item.IconImagePath);
        }
    }
}
