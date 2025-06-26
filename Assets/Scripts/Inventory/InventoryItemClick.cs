using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class InventoryItemClick : MonoBehaviour, IPointerDownHandler
{
    [Inject] private IPanelItemsMenu itemsMenu;
    [SerializeField] private Image itemImage;
     private ISlotModel slotModel;

    private void Awake()
    {
        slotModel = GetComponent<ISlotModel>();
    }

    //клик по предмету в инвентаре
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (slotModel.GetItem() != null)
        {
            itemsMenu.EnableItemPanel(itemImage.sprite, slotModel);
        }
    }
}
