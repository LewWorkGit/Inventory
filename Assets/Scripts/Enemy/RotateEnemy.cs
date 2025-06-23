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
            // �������� � ����
            direction = (target.GetTarget().position - myTransform.position).normalized;


            // �������� ������� �� X
            if (direction.x > 0) // �������� ������
            {
                lookVector.Set(1, 1, 1);
                transform.localScale = lookVector; // ������������ �������
            }
            else if (direction.x < 0) // �������� �����
            {
                lookVector.Set(-1, 1, 1);
                transform.localScale = lookVector; // ��������� �� X
            }
        }
    }
}

