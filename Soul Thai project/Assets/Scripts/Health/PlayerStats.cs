using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    public HealthBar healthbar;
    public Animator anim;
    public Image black;

    AnimationManager animationManager;

    // Event to notify when the player dies
    public delegate void OnPlayerDeath();
    public static event OnPlayerDeath onPlayerDeath;

    private void Awake()
    {
        animationManager = GetComponentInChildren<AnimationManager>();
        if (animationManager == null)
        {
            Debug.LogError("AnimationManager not found!");
        }
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthbar.SetCurrentHealth(currentHealth);
        animationManager.PlayTargetAnimation("GetHit", true);

        if (currentHealth <= 0)
        {
            Debug.Log("Player Died");
            currentHealth = 0;
            animationManager.PlayTargetAnimation("Death", true);

            // Start the fade coroutine
            StartCoroutine(FadeAndLoadScene());
        }
    }

    public IEnumerator FadeAndLoadScene()
    {
        // Trigger the fade animation
        anim.SetBool("Fade", true);

        // Wait until the fade is complete (black screen)
        yield return new WaitUntil(() => black.color.a == 1);

        // Trigger the death event
        onPlayerDeath?.Invoke();

        // Load the "Die" scene
        if (Application.CanStreamedLevelBeLoaded("Die"))
        {
            Debug.Log("Loading Death Scene");
            SceneManager.LoadScene("Die");
        }
        else
        {
            Debug.LogError("Death Scene not found in build settings!");
        }
    }
}