using DG.Tweening;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPan�l;

    public void GameOverPanelActive()
    {

        gameOverPan�l.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(gameOverPan�l.transform.DOLocalMove(Vector3.zero, 1f));
        sequence.Append(gameOverPan�l.transform.DOScale(Vector3.one * 1.01f, 0.5f));

    }
}
