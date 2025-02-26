using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour


{
    public string targetbool;
    public bool status;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetBool(targetbool, status);
    }
}