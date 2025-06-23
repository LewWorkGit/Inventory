using UnityEngine;
public class InventorySlotModel : MonoBehaviour, ISlotModel
{
    private Items item;
    private ISlotViev slotViev;

    private int countItem;

    private void Awake()
    {
        slotViev = GetComponent<ISlotViev>();
    }
    public Items GetItem()
    {
        return item;
    }

    public int GetCountItem()
    {
        return countItem;
    }

    public void AddItem(Items _item, int quantityAddItem)
    {
        item = _item;
        countItem += quantityAddItem;
        slotViev.AddItem(countItem, _item);
    }

    public void DeleteItem()
    {
        item = null;
        countItem = 0;
        slotViev.DeleteItem();
    }  
}
