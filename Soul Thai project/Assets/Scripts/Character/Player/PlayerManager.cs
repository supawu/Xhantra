using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    public Animator anim;
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        anim=GetComponentInChildren<Animator>();
    }

    void Update()
    {
        inputManager.isInteract = anim.GetBool("isInteract");
        inputManager.rollFlag = false;
    }
}
