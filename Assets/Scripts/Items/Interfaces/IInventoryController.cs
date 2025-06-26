using UnityEngine.InputSystem;
public interface IInventoryController
{
    public void OpenCloseEnventory(InputAction.CallbackContext context);
    public void AddItem(Items item, int quantityAddItem);
}
