using UnityEngine;
using UnityEngine.UI;

public class GambingSlot : MonoBehaviour
{
    public int number;
    public Image img;

    private void Update()
    {
        switch(number)
        {
            case 1: img.sprite = Utils.LoadSprite(Path.Number1ImgPath);break;
            case 2: img.sprite = Utils.LoadSprite(Path.Number2ImgPath); break;
            case 3: img.sprite = Utils.LoadSprite(Path.Number3ImgPath); break;
            case 4: img.sprite = Utils.LoadSprite(Path.Number4ImgPath); break;
            case 5: img.sprite = Utils.LoadSprite(Path.Number5ImgPath); break;
            case 6: img.sprite = Utils.LoadSprite(Path.Number6ImgPath); break;
            default:img.sprite = Utils.LoadSprite(Path.DefaultImgPath);break;
        }
    }
}
