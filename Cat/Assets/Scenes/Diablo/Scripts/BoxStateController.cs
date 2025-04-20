using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    // TODO: finish Box State
    /**
      Idle -> WaitInteraction: when clicked 
      WaitInteraction -> AttackState: when collision triggered AND BoxConfig.content == Monster
      AttackState -> Idle: when out of collision
      WaitInteraction -> OpenState: when collision triggered AND (BoxConfig.content == Memory OR BoxConfig.content == Health), memoryBox disappear after opened
      OpenState -> Idel: when out of collision
    **/
    public class BoxStateController : MonoBehaviour
    {
        public enum BoxState
        {
            Idle,
            WaitInteraction,
            AttackState,
            OpenState
        }

        public BoxState currentState = BoxState.Idle;

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

        void HandleIdle()
        {
               
        }

        void HandleWaitInteraction()
        {

        }

        void HandleAttackState()
        {

        }

        void HandleOpenState()
        {

        }

        void TransitionToState(BoxState newState)
        {
            Debug.Log($"Transitioning from {currentState} to {newState}");
            currentState = newState;
        }
    }

}
