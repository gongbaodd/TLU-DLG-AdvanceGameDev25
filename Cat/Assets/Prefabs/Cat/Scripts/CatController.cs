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
    }

    /** LifeCycle **/
    void Start()
    {
        InitCat();
    }
}
