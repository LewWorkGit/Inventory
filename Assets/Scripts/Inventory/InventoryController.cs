using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InventoryController : MonoBehaviour
{
    [Inject] private List<ISlotModel> slotsModel;
    [SerializeField] private CanvasGroup inventoryPanel;
    private bool isOpenInventar;

    //��������� ��� ��������� ���������
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


    //��������� ������� � ���������
    public void AddItem(Items item, int quantityAddItem)
    {
        //���� ��� ���� ����� ������� ��������� � ����
        foreach (var slot in slotsModel)
        {
            if (slot.GetItem() == item)
            {

                slot.AddItem(item, quantityAddItem);
                return;
            }
        }
        //���� ��� ������ �������� ��������� � ������ ����
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
