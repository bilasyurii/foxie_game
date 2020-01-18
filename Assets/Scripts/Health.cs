using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;

    public int health { get; private set; }
    [HideInInspector] public bool invincible = false;

    private IHealthAnimationManager healthAnimManager;

    private void Awake()
    {
        healthAnimManager = GetComponent<IHealthAnimationManager>();

        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (invincible)
            return;

        health -= damage;

        healthAnimManager?.PlayHurt(damage);

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        healthAnimManager?.PlayDeath();

        Destroy(gameObject);
    }
}
