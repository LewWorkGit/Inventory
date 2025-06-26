using UnityEngine;
using Zenject;

public class PlayerDie : MonoBehaviour, IDieable
{
    private IMovePlayer movePlayer;
    private IPlayerAnimation playerAnimations;
    [Inject] private GameOver gameOver;
    [Inject] private SaveLoadManager saveLoadManager;

    private void Awake()
    {
        movePlayer = GetComponent<IMovePlayer>();
        playerAnimations = GetComponent<IPlayerAnimation>();
    }

    public void Die()
    {
        saveLoadManager.SetSaveExit(false);
        movePlayer.DisableInput();
        playerAnimations.DiePlayer();
        gameOver.GameOverPanelActive();
        ES3.DeleteFile();
       
    }
}
