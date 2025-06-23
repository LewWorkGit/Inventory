using UnityEngine;
using UnityEngine.UI;

public class ItemsMenu : MonoBehaviour, IPanelItemsMenu
{
    private ISlotModel slot;

    [SerializeField] private GameObject itemPanelMenu;
    [SerializeField] Image itemImage;

    public void EnableItemPanel(Sprite itemSprite, ISlotModel slotModel)
    {
        itemPanelMenu.SetActive(true);
        itemImage.sprite = itemSprite;
        slot = slotModel;
    }

    public void DeleteItem()
    {
        slot.DeleteItem();
        itemPanelMenu.SetActive(false);
    }
}
