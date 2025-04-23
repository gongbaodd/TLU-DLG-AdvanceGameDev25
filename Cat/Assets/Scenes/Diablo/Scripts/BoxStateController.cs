using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class BoxStateController : MonoBehaviour
    {
        Animator anim;

        public enum BoxState
        {
            Idle,
            WaitInteraction,
            AttackState,
            OpenState,
            DefeatedState
        }

        public BoxState currentState = BoxState.Idle;

        
        void HandleIdle()
        {
            if (interacted)
            {
                TransitionToState(BoxState.WaitInteraction);
            }
        }

        void HandleWaitInteraction()
        {
            var boxConfig = GetComponent<BoxController>().boxConfig;

            if (playerIsAround)
            {
                ClearInteract();
                
                if (boxConfig.content == BoxContent.Monster)
                {
                    TransitionToState(BoxState.AttackState);
                }
                else
                {
                    TransitionToState(BoxState.OpenState);
                }
            }
            else if (interacted == false)
            {
                TransitionToState(BoxState.Idle);
            }
        }

        void HandleAttackState()
        {
            var attackRoutine = KeepAttack();
            StartCoroutine(attackRoutine);

            if (playerIsAround == false)
            {
                StopCoroutine(attackRoutine);
                TransitionToState(BoxState.Idle);
            }
        }

        void HandleOpenState()
        {
            if (playerIsAround == false)
            {
                TransitionToState(BoxState.Idle);
            }
        }

        void TransitionToState(BoxState newState)
        {
            Debug.Log($"Transitioning from {currentState} to {newState}");
            currentState = newState;
        }


        bool interacted = false;

        public void Interact()
        {
            interacted = true;
        }

        public void ClearInteract()
        {
            interacted = false;
        }

        bool playerIsAround = false;

        public void PlayerCome()
        {
            playerIsAround = true;
        }

        public void PlayerLeave()
        {
            playerIsAround = false;
        }

        IEnumerator KeepAttack() {
            anim.SetTrigger("Attack");

            var boxConfig = GetComponent<BoxController>().boxConfig;
            yield return new WaitForSeconds(boxConfig.attackInterval);
        }

        
        void Awake()
        {
            anim = GetComponent<Animator>();
        }


        void Update()
        {
            switch (currentState)
            {
                case BoxState.Idle:
                    HandleIdle();
                    break;
                case BoxState.WaitInteraction:
                    HandleWaitInteraction();
                    break;
                case BoxState.AttackState:
                    HandleAttackState();
                    break;
                case BoxState.OpenState:
                    HandleOpenState();
                    break;
            }
        }

    }

}
