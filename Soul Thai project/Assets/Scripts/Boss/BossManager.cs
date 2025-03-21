using UnityEngine;

public class BossManager : MonoBehaviour
{
    public bool isInteracting;
    public bool isDead;

    private BossLocomotion locomotion;
    private BossAI ai;
    private BossAnimationManager animationManager;
    private Rigidbody rb;
        AudioManager audioManager;


    private void Awake()
    {
        locomotion = GetComponent<BossLocomotion>();
        ai = GetComponent<BossAI>();
        animationManager = GetComponentInChildren<BossAnimationManager>();
        audioManager = GetComponent<AudioManager>();

        rb = GetComponent<Rigidbody>();
    }

    /*public void HandleDamageInteraction()
    {
        if (isDead) return; // Ignore damage if already dead

        // Unfreeze Rigidbody constraints temporarily
        /*if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        // Trigger "GetHit" animation
        //animationManager.PlayTargetAnimation("GetHit", true);
        //isInteracting = true;
        audioManager.PlaySFX(audioManager.attack2);

        Debug.Log("Boss handled damage interaction!");
    } */

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
            rb.constraints = RigidbodyConstraints.FreezeAll; // Freeze position and rotation
        }

        Debug.Log("Boss handled death!");
    }

    public void ResetInteracting()
    {
        isInteracting = false;

        // Re-freeze Rigidbody constraints after the animation
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}