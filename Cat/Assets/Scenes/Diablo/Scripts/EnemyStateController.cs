using System.Collections;
using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(SkeletonAnimatorController))]
    [RequireComponent(typeof(SkeletonMovementController))]
    public class EnemyStateController : MonoBehaviour
    {
        public enum EnemyState
        {
            Idle,
            Move,
            Catch,
            Attack,
            Defeated
        }

        public EnemyState currentState = EnemyState.Idle;

        bool isWalking = false;

        void StartWalking() {
            movementController.PickNewDestination();
            isWalking = true;
        }

        
        void HandleIdle()
        {
            aniController.Stand();

            if (isWalking)
            {
                TransitionToState(EnemyState.Move);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                TransitionToState(EnemyState.Catch);
            }
        }

        IEnumerator Waiting() {
            yield return new WaitForSeconds(config.waitTime);
            StartWalking();
        }

        void HandleMove()
        {
            aniController.Walk();

            if (movementController.IsWalking() == false)
            {
                isWalking = false;
                TransitionToState(EnemyState.Idle);
                StartCoroutine(Waiting());
            }
            else if (isCatch)
            {
                TransitionToState(EnemyState.Catch);
            }
        }

        bool isCatch = false;

        public void Catch() {
            isCatch = true;
        }

        void HandleCatch()
        {
            aniController.Run();
            if (Input.GetKeyDown(KeyCode.M))
            {
                TransitionToState(EnemyState.Move);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                TransitionToState(EnemyState.Attack);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                TransitionToState(EnemyState.Idle);
            }
        }

        void HandleAttack()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                TransitionToState(EnemyState.Catch);
            }
        }

        void HandleDefeated()
        {
            Debug.Log("Enemy defeated. No more actions.");
            // Optionally disable this GameObject or stop all logic
        }

        void TransitionToState(EnemyState newState)
        {
            Debug.Log($"Enemy transitioning from {currentState} to {newState}");
            currentState = newState;
        }

        SkeletonAnimatorController aniController;
        SkeletonMovementController movementController;
        EnemyConfig config;
        void Awake()
        {
            aniController = GetComponent<SkeletonAnimatorController>();
            movementController = GetComponent<SkeletonMovementController>();
            var controller = GetComponent<SkeletonController>();
            config = controller.config;
        }

        void Start()
        {
            StartWalking();
        }

        void Update()
        {
            // Global defeated trigger
            if (Input.GetKeyDown(KeyCode.K))
            {
                TransitionToState(EnemyState.Defeated);
                return;
            }

            switch (currentState)
            {
                case EnemyState.Idle:
                    HandleIdle();
                    break;
                case EnemyState.Move:
                    HandleMove();
                    break;
                case EnemyState.Catch:
                    HandleCatch();
                    break;
                case EnemyState.Attack:
                    HandleAttack();
                    break;
                case EnemyState.Defeated:
                    HandleDefeated();
                    break;
            }
        }

    }
}
