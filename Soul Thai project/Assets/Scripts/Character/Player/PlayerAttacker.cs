using Unity.Profiling;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    AnimationManager animatorHandler;
    AudioManager audioManager;
    Locomotion locomotion;
    Rigidbody rb;
    
    public void Awake()
    {
        animatorHandler = GetComponentInChildren<AnimationManager>();
        audioManager = GetComponent<AudioManager>();
        locomotion = GetComponent<Locomotion>();
        rb = GetComponent<Rigidbody>();
    }
    
    public void HandleLightAttack(WeaponItem weapon)
    {
        if (!animatorHandler.anim.GetBool("isInteract")) // Prevent attacking during interactions
        {
            // Stop movement
            StopMovement();
            
            // Play attack animation
            animatorHandler.PlayTargetAnimation(weapon.MeleeAttack_OneHanded, true);
            audioManager.PlaySFX(audioManager.hit);
        }
    }
    
    public void HandleHeavyAttack(WeaponItem weapon)
    {
        if (!animatorHandler.anim.GetBool("isInteract")) // Prevent attacking during interactions
        {
            // Stop movement
            StopMovement();
            
            // Play attack animation
            animatorHandler.PlayTargetAnimation(weapon.MeleeAttack_TwoHanded, true);
            audioManager.PlaySFX(audioManager.hardHit);
        }
    }
    
    // New function to stop player movement
    private void StopMovement()
    {
        // Set velocity to zero to stop movement immediately
        if (rb != null)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0); // Preserve vertical velocity for gravity
        }
    }
}