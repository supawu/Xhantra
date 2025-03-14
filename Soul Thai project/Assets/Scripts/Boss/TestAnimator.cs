using UnityEngine;

public class TestAnimator : MonoBehaviour
{
    public Animator anim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("GetHit");
            Debug.Log("GetHit triggered!");
        }
    }
}