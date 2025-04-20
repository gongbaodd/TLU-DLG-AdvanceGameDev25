using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
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

        void HandleIdle()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                TransitionToState(EnemyState.Move);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                TransitionToState(EnemyState.Catch);
            }
        }

        void HandleMove()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                TransitionToState(EnemyState.Idle);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                TransitionToState(EnemyState.Catch);
            }
        }

        void HandleCatch()
        {
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
    }
}
