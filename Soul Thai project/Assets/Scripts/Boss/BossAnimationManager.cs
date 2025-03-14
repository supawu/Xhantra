using UnityEngine;

public class BossAnimationManager : MonoBehaviour
{
    public Animator anim;
    public bool canRotate;

    private int vertical;
    private int horizontal;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
    }

    public void CanRotate()
    {
        canRotate = true;
    }

    public void StopRotation()
    {
        canRotate = false;
    }
}