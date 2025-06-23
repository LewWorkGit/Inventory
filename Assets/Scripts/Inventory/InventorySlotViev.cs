using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotViev : MonoBehaviour, ISlotViev
{
    [SerializeField] private Image slotImage;
    [SerializeField] private TextMeshProUGUI itemCountText;

    public void AddItem(int itemCountValue, Items _item)
    {

        slotImage.enabled = true;
        slotImage.sprite = _item.GetSpriteItem();

        if (itemCountValue > 1)
        {
            itemCountText.text = itemCountValue.ToString();
        }
    }

    public void DeleteItem()
    {
        slotImage.enabled = false;
        slotImage.sprite = null;
        itemCountText.text = "";
    }
}
