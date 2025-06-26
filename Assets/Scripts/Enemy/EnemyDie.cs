using UnityEngine;
using UnityEngine.AI;

public class EnemyDie : MonoBehaviour, IDieable
{
     private IEnemyAnimations animations;
     private IEnemy enemy;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Collider2D myCollider;

    private void Awake()
    {
        enemy = GetComponent<IEnemy>();
        animations = GetComponent<IEnemyAnimations>();
    }
    //смерть врага
    public void Die()
    {
        enemy.StopChasing();
        animations.Die();
        myCollider.enabled = false;
    }
}
