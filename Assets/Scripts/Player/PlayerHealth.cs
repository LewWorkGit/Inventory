using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;
    [Inject] private IHealthBar healthBar; 
    [Inject] private SaveLoadManager saveLoadManager;
    private IDieable diePlayer;

    private void Awake()
    {
        diePlayer = GetComponent<IDieable>();
    }
    private void OnEnable()
    {
        saveLoadManager.OnLoadOver += RefreshHealBar;
    }
    private void OnDisable()
    {
        saveLoadManager.OnLoadOver -= RefreshHealBar;
    }

    //нанесение урона игроку
    void IDamageable.Damage(int damageValue)
    {
        if (health > 0)
        {
            health -= damageValue;
            health = Mathf.Clamp(health, 0, 100);
            healthBar.RefreshHealthBar(health);

            //смерть игрока
            if (health == 0)
            {
                diePlayer.Die();
            }
        }

    }

    private void RefreshHealBar()
    {
        healthBar.RefreshHealthBar(health);
    }
}
