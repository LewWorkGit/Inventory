using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponsAmmoText;
    [SerializeField] private TextMeshProUGUI inventoryAmmoText;

    public void SetWeaponAmmoText(int weaponAmmoValue)
    {
        weaponsAmmoText.text = weaponAmmoValue.ToString();
    }

    public void SetInvetoryAmmoText(int inventoryAmmoValue)
    {
        inventoryAmmoText.text = inventoryAmmoValue.ToString();
    }
}
