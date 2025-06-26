using UnityEngine;

public class EnemyAnimations : MonoBehaviour, IEnemyAnimations
{
    [SerializeField] private Animator animator;


    public void Idle()
    {
        animator.SetTrigger("idle");
    }

    public void Run()
    {
        animator.SetTrigger("run");
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }

    public void Die()
    {
        animator.SetTrigger("die");
    }

}
