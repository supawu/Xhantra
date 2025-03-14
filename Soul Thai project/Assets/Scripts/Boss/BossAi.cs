using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float attackRange = 3f; // Distance at which the Boss attacks
    public float attackCooldown = 2f; // Time between attacks
    private float lastAttackTime;

    private BossManager bossManager;
    private BossAnimationManager animationManager;
    private Transform player;

    private void Awake()
    {
        bossManager = GetComponent<BossManager>();
        animationManager = GetComponentInChildren<BossAnimationManager>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        // Trigger attack animation
        animationManager.PlayTargetAnimation("Attack", true);

        // Set cooldown
        lastAttackTime = Time.time;

        Debug.Log("Boss is attacking the player!");
    }
}