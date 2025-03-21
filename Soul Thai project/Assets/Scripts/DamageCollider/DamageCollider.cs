using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;
    public int damage = 25; // Base damage value
    public bool isPlayerWeapon; // Set this in the Inspector to differentiate player/boss weapons
    
    // Add this to track if we've already hit something during this attack
    private bool hasHitDuringThisAttack = false;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        if (damageCollider == null)
        {
            Debug.LogError("Collider component missing on: " + gameObject.name);
            return;
        }

        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false; // Disabled by default

        Debug.Log("Damage collider initialized on: " + gameObject.name + " (IsPlayerWeapon: " + isPlayerWeapon + ")");
    }

    public void EnableDamageCollider()
    {
        if (damageCollider != null)
        {
            damageCollider.enabled = true;
            // Reset hit tracking when we enable the collider for a new attack
            hasHitDuringThisAttack = false;
            Debug.Log("Damage collider enabled on: " + gameObject.name + " (IsPlayerWeapon: " + isPlayerWeapon + ")");
        }
        else
        {
            Debug.LogError("Damage collider is null on: " + gameObject.name);
        }
    }

    public void DisableDamageCollider()
    {
        if (damageCollider != null)
        {
            damageCollider.enabled = false;
            Debug.Log("Damage collider disabled on: " + gameObject.name + " (IsPlayerWeapon: " + isPlayerWeapon + ")");
            // For debugging - comment this out in production
            Debug.Log("Disable call stack: " + System.Environment.StackTrace);
        }
        else
        {
            Debug.LogError("Damage collider is null on: " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision detected on: " + gameObject.name + " with: " + collision.name + " (Tag: " + collision.tag + ")");

        // Player weapon hits boss
        if (isPlayerWeapon && collision.CompareTag("Enemy"))
        {
            // Do NOT disable the weapon here
            Debug.Log("Player hit boss: " + collision.name);
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damage);
                // Option 1: Allow multiple hits (remove any code that disables the collider)
                
                // Option 2: If you want to prevent multiple hits per swing but NOT disable the collider yet:
                /*
                if (!hasHitDuringThisAttack) {
                    enemyStats.TakeDamage(damage);
                    hasHitDuringThisAttack = true;
                }
                */
            }
            else
            {
                Debug.LogError("EnemyStats component not found on: " + collision.name);
            }
        }
        // Boss weapon hits player
        else if (!isPlayerWeapon && collision.CompareTag("Player"))
        {
            Debug.Log("Boss hit player: " + collision.name);
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
            else
            {
                Debug.LogError("PlayerStats component not found on: " + collision.name);
            }
        }
    }
}