using UnityEngine;

public class CatController : MonoBehaviour
{
    private Animator animator;

    private void InitCat()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Cat Animator not found");
        }
    }

    public void Stand()
    {
        animator.SetTrigger("stand");
        animator.SetFloat("speed", 0.0f);
    }

    public void Walk()
    {
        animator.SetFloat("speed", 0.1f);
    }

    /** LifeCycle **/
    void Start()
    {
        InitCat();
    }
}
