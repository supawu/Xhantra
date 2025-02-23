using Unity.VisualScripting;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    PlayerManager playerManager;
    public Animator anim;
    InputManager inputManager;
    Locomotion playerLocomotion;
    int vertical;
    int horizontal;
    public bool canRotate;

    public void Initialize()
    {   
        playerManager = GetComponentInParent<PlayerManager>();
        anim = GetComponent<Animator>();
        inputManager=GetComponentInParent<InputManager>();
        playerLocomotion = GetComponentInParent<Locomotion>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
        canRotate=true; //Ensure it starts as true (Rolling Fixed)

    }
    public void UpdateAnimatorValues(float vericalMovement,float horizontalMovement)
    {
        #region Vertical
        float v = 0;

        if (vericalMovement>0 && vericalMovement < 0.55f)
        {
            v = 0.5f;
        }
        else if (vericalMovement > 0.55f)
        {
            v=1;
        }
        else if (vericalMovement < 0 && vericalMovement >-0.55f)
        {
            v = -0.5f;
        }
        else if (vericalMovement < -0.55f)
        {
            v=-1;
        }
        else
        {
            v=0;
        }
        #endregion

        #region Horizontal
        float h =0;
        if (horizontalMovement>0 && horizontalMovement < 0.55f)
        {
            h = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            h=1;
        }
        else if (horizontalMovement < 0 && horizontalMovement >-0.55f)
        {
            h = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            h=-1;
        }
        else
        {
            h=0;
        }
        #endregion
        
        anim.SetFloat(vertical,v,0.1f,Time.deltaTime);
        anim.SetFloat(horizontal,h,0.1f,Time.deltaTime);

    }

    public void PlayTargetAnimation(string targetAnim , bool isInteract)
    {
        anim.SetBool("isInteract",isInteract);
        anim.CrossFade(targetAnim,0.2f);

    }
      
        
    public void CanRotate()
    {
        canRotate = true;
    }
    public void StopRotation()
    {
        canRotate=false;
    }


     public void OnRollAnimationEnd()
    {
        anim.SetBool("isInteract", false);
        anim.applyRootMotion = false;
        canRotate = true;
        if (inputManager != null)
        {
            playerManager.isInteract = false;
        }
    }

    [System.Obsolete]
    private void OAnimatorMove()
    {
        if(playerManager.isInteract == false)
            return;

        float delta = Time.deltaTime;
        playerLocomotion.rigidbody.drag =0;
        Vector3 deltaPosition = anim.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition /delta;
        playerLocomotion.rigidbody.linearVelocity = velocity;
    }
}
