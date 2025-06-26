using UnityEngine;

public class PlayerAnimation : MonoBehaviour,IPlayerAnimation
{
    private Rigidbody2D rbPlayer;
    [SerializeField]private Animator animator;


    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        WalkAnim();
    }

    //анимация ходьбы
    private void WalkAnim()
    {
        
        if (rbPlayer.velocity != Vector2.zero)
        {
            animator.SetTrigger("walk");
        }
        else
        {
            animator.SetTrigger("idle");
        }
    }

    //анимация смерти
    public void DiePlayer()
    {
        animator.SetTrigger("die");
    }
}
