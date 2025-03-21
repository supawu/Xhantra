using UnityEngine;

public class BossManager : MonoBehaviour {
    public bool isInteracting;
    public bool isDead;
    
    private BossLocomotion locomotion;
    private BossAI ai;
    private BossAnimationManager animationManager;
    private Rigidbody rb;
    public Animator anim;
    AudioManager audioManager;
    
    private void Awake()
    {
        locomotion = GetComponent<BossLocomotion>();
        ai = GetComponent<BossAI>();
        animationManager = GetComponentInChildren<BossAnimationManager>();
        audioManager = GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody>();
    }
    
    // Add this back with modified implementation
    public void HandleDamageInteraction()
    {
        if (isDead) return;
        
        // Ensure boss is active if it wasn't already
        if (ai != null && !ai.isActive)
        {
            ai.ActivateBoss();
        }
        
        // Play sound effect if available
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.attack2);
        }
        
        Debug.Log("Boss handled damage interaction!");
    }
    
    public void HandleDeath()
    {
        isDead = true;
        
        // Trigger "Death" animation
        animationManager.PlayTargetAnimation("Death", true);
        isInteracting = true;
        
        // Disable movement and AI
        if (locomotion != null) locomotion.enabled = false;
        if (ai != null) ai.enabled = false;
        
        // Disable Rigidbody physics
        if (rb != null)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        
        Debug.Log("Boss handled death!");
    }
    
    public void ResetInteracting()
    {
        isInteracting = false;
        
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}