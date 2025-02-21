using Unity.VisualScripting;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animate;
    int vertical;
    int horizontal;
    public bool canRotate;

    public void Initialize()
    {
        animate = GetComponent<Animator>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");

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
        
        animate.SetFloat(vertical,v,0.1f,Time.deltaTime);
        animate.SetFloat(horizontal,h,0.1f,Time.deltaTime);

    }

    public void CanRotate()
    {
        canRotate = true;
    }
    public void StopRotation()
    {
        canRotate=false;
    }
}
