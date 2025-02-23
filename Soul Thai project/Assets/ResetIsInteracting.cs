using UnityEngine;

public class ResetIsInteracting : StateMachineBehaviour
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
        InputManager inputManager = animator.GetComponentInParent<InputManager>();
        if (inputManager != null)
        {
            inputManager.isInteract = false;
        }
    }
}