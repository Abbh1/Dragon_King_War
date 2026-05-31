using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OriginMaterialSlot : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public UnityEvent rightClick;
    public UnityEvent leftClick;
    private Image img;
    private void Start()
    {
        img = GetComponent<Image>();
        rightClick.AddListener(new UnityAction(ButtonRightClick));
        leftClick.AddListener(new UnityAction(ButtonLeftClick));
    }
    private void Update()
    {
        if(item.Id==0)
        {
            img.sprite= Utils.LoadSprite(Path.DefaultImgPath);
        }
        else
        {
            img.sprite = Utils.LoadSprite(item.IconImagePath);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right && item.Id != 0)
            rightClick.Invoke();
    }
    public void  ButtonLeftClick()
    {
        ForgeController.instance.selectedSlot = GetComponent<OriginMaterialSlot>();
        GameController.instance.SetGameObjectActive(GameController.instance.bagPanel);
        Invoke("SetBagModeForgeSelect",0.1f);
    }
    private void SetBagModeForgeSelect()
    {
        BagSlotsController.instance.mode = BagSlotsController.Mode.ForgeSelect;
    }
    private void ButtonRightClick()
    {
        BagManager.instance.AddItemToBag(item, 1, false);
        item = new Item();
    }
}
