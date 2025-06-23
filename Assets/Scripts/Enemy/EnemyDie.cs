using UnityEngine;
using UnityEngine.AI;

public class EnemyDie : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Enemy enemy;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Collider2D myCollider;

    //смерть врага
    public void DieEnemy()
    {
        enemy.StopChasing();
        animator.SetTrigger("die");
        myCollider.enabled = false;
    }
}
