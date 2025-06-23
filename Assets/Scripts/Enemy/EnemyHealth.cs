using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private int enemyHelth = 50;
    [SerializeField] private EnemyDie enemyDie;
    [SerializeField] private EnemyHealthBar enemyHealthBar;

    void IDamageable.Damage(int damageValue)
    {
        if (enemyHelth > 0)
        {
            enemyHelth -= damageValue;
            enemyHelth = Mathf.Clamp(enemyHelth, 0, 100);
            enemyHealthBar.RefreshHealthBar(enemyHelth);

            if (enemyHelth == 0)
            {
                enemyDie.DieEnemy();
                Destroy(gameObject, 1);
            }
        }
    }

    public int GetEnemyHealth()
    {
        return enemyHelth;
    }

}
