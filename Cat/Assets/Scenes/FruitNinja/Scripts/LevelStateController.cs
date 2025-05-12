
using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{
    public class LevelStateController : MonoBehaviour
    {
        struct Ctx
        {
            public bool isGaming;
        }
        public enum State
        {
            Game,
            Story,
        }
        Ctx ctx = new()
        {
            isGaming = false
        };
        public State currentState = State.Story;
        void HandleStoryState()
        {
            if (ctx.isGaming)
            {
                TranslateState(State.Game);
            }
        }

        void HandleGameState()
        {
            if (ctx.isGaming == false)
            {
                TranslateState(State.Story);
            }

        }
        void TranslateState(State newState)
        {
            currentState = newState;
        }
        void Update()
        {
            switch (currentState)
            {
                case State.Story:
                    HandleStoryState();
                    break;
                case State.Game:
                    HandleGameState();
                    break;
            }
        }
    }
}