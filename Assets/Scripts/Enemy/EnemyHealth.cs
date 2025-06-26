using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private int enemyHelth = 50;
    private IDieable enemyDie;
    private IHealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponent<IHealthBar>();
        enemyDie = GetComponent<IDieable>();
    }

    void IDamageable.Damage(int damageValue)
    {
        if (enemyHelth > 0)
        {
            enemyHelth -= damageValue;
            enemyHelth = Mathf.Clamp(enemyHelth, 0, 100);
            healthBar.RefreshHealthBar(enemyHelth);

            if (enemyHelth == 0)
            {
                enemyDie.Die();
                Destroy(gameObject, 1);
            }
        }
    }

    public int GetEnemyHealth()
    {
        return enemyHelth;
    }

}
