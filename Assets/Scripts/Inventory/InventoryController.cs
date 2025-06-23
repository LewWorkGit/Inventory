using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InventoryController : MonoBehaviour
{
    [Inject] private List<ISlotModel> slotsModel;
    [SerializeField] private CanvasGroup inventoryPanel;
    private bool isOpenInventar;

    //открывает или закрывает инвентарь
    public void OpenCloseEnventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isOpenInventar)
            {
                inventoryPanel.alpha = 1;
                inventoryPanel.blocksRaycasts = true;
                isOpenInventar = true;
            }
            else
            {
                inventoryPanel.alpha = 0;
                inventoryPanel.blocksRaycasts = false;
                isOpenInventar = false;
            }
        }
    }


    //добовляем предмет в инвентарь
    public void AddItem(Items item, int quantityAddItem)
    {
        //если уже есть такой предмет добавляем в стак
        foreach (var slot in slotsModel)
        {
            if (slot.GetItem() == item)
            {

                slot.AddItem(item, quantityAddItem);
                return;
            }
        }
        //если нет такого предмета добавляем в пустой слот
        foreach (var slot in slotsModel)
        {
            if (slot.GetItem() == null)
            {

                slot.AddItem(item, quantityAddItem);
                return;
            }
        }
    }

}
