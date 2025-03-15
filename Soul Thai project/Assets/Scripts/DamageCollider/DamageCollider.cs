using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;
    public int currentWeapomDamge = 25;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }
    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
{
    Debug.Log("Collision detected with: " + collision.name + " (Tag: " + collision.tag + ")");

    if (collision.CompareTag("Player"))
    {
         Debug.Log("Collision detected with: " + collision.name + " (Tag: " + collision.tag + ")");

    if (collision.CompareTag("Player"))
    {
        Debug.Log("Player hit detected");
        PlayerStats playerStats = collision.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            playerStats.TakeDamage(currentWeapomDamge);
        }
        else
        {
            Debug.LogError("PlayerStats component not found on: " + collision.name);
        }
    }

    if (collision.CompareTag("Enemy"))
    {
        Debug.Log("Enemy hit detected with: " + collision.name);
        EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

        if (enemyStats != null)
        {
            Debug.Log("EnemyStats component found on: " + collision.name);
            enemyStats.TakeDamage(currentWeapomDamge);
        }
        else
        {
            Debug.LogError("EnemyStats component not found on: " + collision.name);
        }
    }
    }
}
}
