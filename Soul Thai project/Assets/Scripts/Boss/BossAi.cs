using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float attackRange = 3f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    public bool isActive = false; // 🚨 New: Boss only activates when player enters the room

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
        if (!isActive || player == null || bossManager.isDead || bossManager.isInteracting) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        string[] attackAnimations = { "Attack1", "Attack2", "Attack3" };
        string randomAttack = attackAnimations[Random.Range(0, attackAnimations.Length)];

        animationManager.PlayTargetAnimation(randomAttack, true);
        lastAttackTime = Time.time;

        Debug.Log("Boss is attacking the player with " + randomAttack + "!");
    }

    // 🚨 New: Activate boss AI when triggered
    public void ActivateBoss()
    {
        isActive = true;
        Debug.Log("Boss is now active!");
    }
}
