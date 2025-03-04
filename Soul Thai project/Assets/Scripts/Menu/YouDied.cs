using UnityEngine;

public class YouDied : MonoBehaviour
{
    [SerializeField] GameObject DieScreen;

    void Start()
    {
        DieScreen.SetActive(false);

        // Subscribe to the player death event
        PlayerStats.onPlayerDeath += ActivateDie;
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        PlayerStats.onPlayerDeath -= ActivateDie;
    }

    public void ActivateDie()
    {
        DieScreen.SetActive(true);
        Debug.Log("Player has died. Activating DieScreen.");
    }
}