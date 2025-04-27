using System.Collections;
using UnityEngine;

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

        bool isAttacking = false;
        IEnumerator storedAttack;
        void HandleAttackState()
        {
            if (!isAttacking) {
                storedAttack = KeepAttack();
                StartCoroutine(storedAttack);
                isAttacking = true;
            }


            if (playerIsAround == false)
            {
                isAttacking = false;
                StopCoroutine(storedAttack);
                TransitionToState(BoxState.Idle);
            }
        }

        void HandleOpenState()
        {
            var boxConfig = GetComponent<BoxController>().boxConfig;

            if (boxConfig.content == BoxContent.Memory)
            {
                var manager = DiabloController.gameManager;
                if (manager)
                {
                    manager.GetComponent<DiabloController>().Win();
                }
            }


            if (playerIsAround == false)
            {
                TransitionToState(BoxState.Idle);
            }
        }

        void TransitionToState(BoxState newState)
        {
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

        IEnumerator KeepAttack()
        {
            while (playerIsAround)
            {
                anim.SetTrigger("Attack");

                AttackThePlayer();

                var boxConfig = GetComponent<BoxController>().boxConfig;
                yield return new WaitForSeconds(boxConfig.attackInterval);
            }
        }

        void AttackThePlayer() {
            var player = DiabloController.player;
            var distance = Vector3.Distance(transform.position, player.transform.position);
            var boxConfig = GetComponent<BoxController>().boxConfig;

            if (distance < boxConfig.attackRange) {
                var lifeController = player.GetComponent<LifeBarController>();
                lifeController.Attacked(boxConfig.attackValue);
            }
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
