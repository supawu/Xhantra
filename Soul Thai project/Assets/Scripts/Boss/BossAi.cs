using UnityEngine;

public class BossAI : MonoBehaviour {
    public float attackRange = 3f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    public bool isActive = false; // Boss only activates when player enters the room
    
    private BossManager bossManager;
    private BossAnimationManager animationManager;
    private BossLocomotion bossLocomotion; // Added reference to locomotion
    private Transform player;
    private Collider bossCollider; // Added reference to collider
    
    private void Awake()
    {
        bossManager = GetComponent<BossManager>();
        animationManager = GetComponentInChildren<BossAnimationManager>();
        bossLocomotion = GetComponent<BossLocomotion>(); // Get locomotion component
        bossCollider = GetComponent<Collider>(); // Get collider component
        
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        // Disable boss components at start
        SetBossActive(false);
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
    
    // New: Enable/disable all boss components
    private void SetBossActive(bool active)
    {
        // If locomotion exists, enable/disable it
        if (bossLocomotion != null)
        {
            bossLocomotion.enabled = active;
        }
        
        // If we have an animator, set a parameter or disable it entirely
        Animator anim = GetComponentInChildren<Animator>();
        if (anim != null)
        {
            anim.SetBool("IsActive", active);
            
            // Optional: Completely disable animator when inactive
            if (!active)
            {
                anim.speed = 0;
            }
            else
            {
                anim.speed = 1;
            }
        }
        
        // Optional: Even disable the collider until activated
        /*if (bossCollider != null && bossCollider != GetComponent<CharacterController>()) // Don't disable character controller if present
        {
            bossCollider.enabled = active;
        }*/
    }
    
    // Activate boss AI when triggered
    public void ActivateBoss()
    {
        isActive = true;
        SetBossActive(true);
        
        // Play activation animation if you have one
        animationManager.PlayTargetAnimation("Activate", false); // Create this animation or use existing one
        
        Debug.Log("Boss is now active!");
    }
}