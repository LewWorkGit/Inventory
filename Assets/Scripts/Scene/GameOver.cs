using DG.Tweening;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanål;

    public void GameOverPanelActive()
    {

        gameOverPanål.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(gameOverPanål.transform.DOLocalMove(Vector3.zero, 1f));
        sequence.Append(gameOverPanål.transform.DOScale(Vector3.one * 1.01f, 0.5f));

    }
}
