using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damageValue = 10;
    [SerializeField] private int attackRadius = 4;
    [SerializeField] private Transform attackPoint;
    private Collider2D targetCollider;
    [SerializeField] private LayerMask layerMask;

    public void MeeleAttack()
    {
        targetCollider = Physics2D.OverlapCircle(attackPoint.position, attackRadius, layerMask);

        if (targetCollider != null)
        {
          targetCollider.GetComponent<IDamageable>().Damage(damageValue);
        }
        
    }
}
