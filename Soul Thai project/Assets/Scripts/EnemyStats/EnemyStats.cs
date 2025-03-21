using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;
    public bool isDead;
    public HealthBar healthBar;
    AudioManager audioManager;

    private BossManager bossManager;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        bossManager = GetComponent<BossManager>();
        audioManager = GetComponent<AudioManager>();

        if (animator == null)
        {
            Debug.LogError("Animator component missing in children!");
        }

        if (bossManager == null)
        {
            Debug.LogError("BossManager component missing!");
        }
    }

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        Debug.Log($"Boss initialized with {maxHealth} health.");
    }

    private int SetMaxHealthFromHealthLevel()
    {
        return healthLevel * 10; // Scale max health based on health level
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Ignore damage if already dead

        
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't go below 0
        healthBar.SetCurrentHealth(currentHealth);
        

        Debug.Log($"Boss took {damage} damage. Current health: {currentHealth}");

        // Trigger "GetHit" animation
        /*if (animator != null)
        {
            animator.SetTrigger("GetHit");
            Debug.Log("GetHit trigger set.");
        }*/

        // Notify BossManager that the Boss is taking damage
        /*if (bossManager != null)
        {
            bossManager.HandleDamageInteraction();
        }*/

        // Check for death
        if (currentHealth <= 0)
        {
            Die();
        }
        audioManager.PlaySFX(audioManager.hitattack);

    }

    private void Die()
    {
        isDead = true;

        // Trigger "Death" animation
        if (animator != null)
        {
            animator.SetTrigger("Death");
            Debug.Log("Die trigger set.");
        }

        // Notify BossManager that the Boss has died
        if (bossManager != null)
        {
            bossManager.HandleDeath();
        }

        // Disable movement and AI
        GetComponent<BossLocomotion>().enabled = false;
        GetComponent<BossAI>().enabled = false;
        GetComponent<Collider>().enabled = false; // Disable collider to prevent further interactions

        Debug.Log("Boss has died!");
    }
}