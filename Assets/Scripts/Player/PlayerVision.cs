using System.Collections;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    private Coroutine visionCor;
    private Collider2D enemy;
    private Transform myTransform;
    [SerializeField] private int visionDistance = 1;
    [SerializeField] private LayerMask visionLayerMask;
    [SerializeField] private MovePlayer player;

    private void Awake()
    {
        myTransform = transform;
        visionCor = StartCoroutine(PlayerVisionCor());
    }

    //зрение игрока
    private IEnumerator PlayerVisionCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            enemy = Physics2D.OverlapCircle(myTransform.position, visionDistance, visionLayerMask);

            if (enemy != null)
            {
                player.SetEnemyTransform(enemy.transform);
            }
            else
            {
                player.SetEnemyTransform(null);
            }
        }
    }
}
