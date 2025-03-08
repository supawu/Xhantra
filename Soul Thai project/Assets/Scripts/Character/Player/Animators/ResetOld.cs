using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isInteract", false);
        animator.applyRootMotion = false;
        
        // Get the AnimationManager and reset its state
        AnimationManager animationManager = animator.GetComponent<AnimationManager>();
        if (animationManager != null)
        {
            animationManager.CanRotate();
        }
        
        // Get the InputManager and reset its state
        PlayerManager playerManager = animator.GetComponentInParent<PlayerManager>();
        if (playerManager != null)
        {
            playerManager.isInteract = false;
        }
    }
}