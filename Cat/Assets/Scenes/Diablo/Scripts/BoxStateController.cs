using System;
using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    // TODO: finish Box State
    /**
      Idle -> WaitInteraction: when clicked 
      WaitInteraction -> Idle: when clicked somewhere else
      WaitInteraction -> AttackState: when collision triggered AND BoxConfig.content == Monster
      AttackState -> Idle: when out of collision
      WaitInteraction -> OpenState: 
                when collision triggered AND (BoxConfig.content == Memory OR BoxConfig.content == Health), 
                show VFX on the box the show feedback
                memoryBox disappear after opened
      OpenState -> Idel: when out of collision
    **/
    public class BoxStateController : MonoBehaviour
    {
        public enum BoxState
        {
            Idle,
            WaitInteraction,
            AttackState,
            OpenState,
            DefeatedState
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
            if (interacted) 
            {
                TransitionToState(BoxState.WaitInteraction);
            }
        }

        void HandleWaitInteraction()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                TransitionToState(BoxState.AttackState);
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                TransitionToState(BoxState.OpenState);
            }
            else if (interacted == false)
            {
                TransitionToState(BoxState.Idle);
            }
        }

        void HandleAttackState()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                TransitionToState(BoxState.DefeatedState);
            }
        }

        void HandleOpenState()
        {
            if (Input.GetKeyDown(KeyCode.I))
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

        public void Interact() {
            interacted = true;
        }

        public void ClearInteract() {
            interacted = false;
        }
    }

}
