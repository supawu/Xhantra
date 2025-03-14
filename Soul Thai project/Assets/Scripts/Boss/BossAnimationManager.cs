using UnityEngine;

public class BossAnimationManager : MonoBehaviour
{
    public Animator anim;
    public bool canRotate;

    private int vertical;
    private int horizontal;
    public BossLocomotion locomotion; // Reference to movement script


    private void Awake()
    {
        anim = GetComponent<Animator>();
        locomotion = GetComponentInParent<BossLocomotion>();

        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
        canRotate = true;
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
    {
        float v = Mathf.Clamp(verticalMovement, -1, 1);
        float h = Mathf.Clamp(horizontalMovement, -1, 1);

        anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteract)
    {
        anim.SetBool("isInteract", isInteract);
        anim.CrossFade(targetAnim, 0.2f);

        if (isInteract)
        {
            canRotate = false; // Stop rotating during attack
            locomotion.StopMovement(); // Stop movement during attack
            Invoke(nameof(ResetInteracting), anim.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    private void ResetInteracting()
    {
        anim.SetBool("isInteract", false);
        canRotate = true; // Allow rotation again
        GetComponentInParent<BossManager>().ResetInteracting();
    }

    public void CanRotate()
    {
        canRotate = true;
        Debug.Log("Rotation Enabled");
    }

    public void StopRotation()
    {
        canRotate = false;
        Debug.Log("Rotation Disabled");
    }
}