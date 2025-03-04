using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    public HealthBar healthbar;

    AnimationManager animationManager;

    // Event to notify when the player dies
    public delegate void OnPlayerDeath();
    public static event OnPlayerDeath onPlayerDeath;

    private void Awake()
    {
        animationManager = GetComponentInChildren<AnimationManager>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthbar.SetCurrentHealth(currentHealth);

        animationManager.PlayTargetAnimation("GetHit", true);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animationManager.PlayTargetAnimation("Death", true);

            // Trigger the death event
            onPlayerDeath?.Invoke();
        }
    }
}