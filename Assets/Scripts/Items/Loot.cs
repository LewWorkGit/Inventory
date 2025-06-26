using UnityEngine;
using Zenject;

public class Loot : MonoBehaviour
{
    [SerializeField] private Items item;
    [SerializeField] private int quantityItem;
    [Inject] private IInventoryController inventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.AddItem(item, quantityItem);
            Destroy(gameObject);
        }
    }

}
