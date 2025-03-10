using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        return healthLevel * 10; 
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        if (animator != null) 
        {
            animator.Play("GetHit");
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (animator != null)
            {
                animator.Play("Death");
            }
        }
    }
}
