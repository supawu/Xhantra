using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public BossAI bossAI;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 🚨 Activate when the player enters
        {
            bossAI.ActivateBoss();
            Destroy(gameObject); // Remove trigger after activation
        }
    }
}