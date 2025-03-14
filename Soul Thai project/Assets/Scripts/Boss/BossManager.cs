using UnityEngine;

public class BossManager : MonoBehaviour
{
    public bool isInteracting;

    private BossLocomotion locomotion;
    private BossAI ai;
    private BossAnimationManager animationManager;

    private void Awake()
    {
        locomotion = GetComponent<BossLocomotion>();
        ai = GetComponent<BossAI>();
        animationManager = GetComponentInChildren<BossAnimationManager>();
    }

    public void TakeDamage(int damage)
    {
        // Trigger "GetHit" animation
        animationManager.PlayTargetAnimation("GetHit", true);
        isInteracting = true;

        Debug.Log("Boss took damage!");
    }

    public void Die()
    {
        // Trigger "Death" animation
        animationManager.PlayTargetAnimation("Death", true);
        isInteracting = true;

        Debug.Log("Boss has died!");
    }
}