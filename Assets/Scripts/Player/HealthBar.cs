using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour,IHealthBar
{
    [SerializeField] private Slider hpBar;

    public void RefreshHealthBar(int hpValue)
    {
        hpBar.value = hpValue;
    }
}
