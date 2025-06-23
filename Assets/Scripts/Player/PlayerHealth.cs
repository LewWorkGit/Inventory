using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;
    [Inject] private PlayerHealthBar healthBar;
    [Inject] private GameOver gameOver;
    [Inject] private SaveLoadManager saveLoadManager;
    [SerializeField] private MovePlayer movePlayer;
    [SerializeField] private PlayerAnimation playerAnimations;


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
            healthBar.ChangeHealthBar(health);

            //смерть игрока
            if (health == 0)
            {
                movePlayer.DisableInput();
                playerAnimations.DiePlayer();
                gameOver.GameOverPanelActive();
                ES3.DeleteFile();
                saveLoadManager.SetSaveExit(false);

            }
        }

    }

    private void RefreshHealBar()
    {
        healthBar.ChangeHealthBar(health);
    }
}
