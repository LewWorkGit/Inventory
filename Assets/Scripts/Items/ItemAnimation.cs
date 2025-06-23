using DG.Tweening;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    private Transform myTransform;

    private void Start()
    {
        myTransform = transform;

        ScaleAnimation();
    }

    private void ScaleAnimation()
    {
        if (this != null)
        {
            myTransform.DOScale(Vector3.one * 0.9f, 0.3f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
