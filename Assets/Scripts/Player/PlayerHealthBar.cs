using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;

    public void ChangeHealthBar(int hpValue)
    {
        hpBar.value = hpValue;
    }
}
