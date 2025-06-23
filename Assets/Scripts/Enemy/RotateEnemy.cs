using UnityEngine;

public class RotateEnemy : MonoBehaviour
{
    private Vector2 direction;
    private Enemy target;
    private Transform myTransform;
    private Vector3 lookVector = Vector3.one;

    void Awake()
    {
        target = GetComponent<Enemy>();
        myTransform = transform;
    }

    void Update()
    {
        EnemyLookVector();
    }

    private void EnemyLookVector()
    {
        if (target.GetTarget() != null)
        {
            // Движение к цели
            direction = (target.GetTarget().position - myTransform.position).normalized;


            // Разворот спрайта по X
            if (direction.x > 0) // Движение вправо
            {
                lookVector.Set(1, 1, 1);
                transform.localScale = lookVector; // Оригинальный масштаб
            }
            else if (direction.x < 0) // Движение влево
            {
                lookVector.Set(-1, 1, 1);
                transform.localScale = lookVector; // Отражение по X
            }
        }
    }
}

