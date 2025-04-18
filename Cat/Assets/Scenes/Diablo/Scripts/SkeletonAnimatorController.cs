using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class SkeletonAnimatorController : MonoBehaviour
    {
        private Animator animator;

        public void Walk() => animator.SetTrigger("walk");

        public void Stand() => animator.SetTrigger("idle");

        public void Punch() => animator.SetTrigger("punch");

        public void Run() => animator.SetTrigger("run");

        void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}


