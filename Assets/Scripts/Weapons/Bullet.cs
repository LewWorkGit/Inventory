using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damageValue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.Damage(damageValue);
        }

        gameObject.SetActive(false);
    }

    public void SetDamageValue(int value)
    {
        damageValue = value;
    }

}
