using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider HealthBar;
    [SerializeField] private EnemyHealth enemyHealth;

    private void Awake()
    {
        HealthBar.maxValue = enemyHealth.GetEnemyHealth();
        HealthBar.value = enemyHealth.GetEnemyHealth();
    }

    public void RefreshHealthBar(int hpValue)
    {
        HealthBar.value = hpValue;
    }



}
