using System;
using UnityEngine;

public class LevelStateController : MonoBehaviour
{
    struct Ctx // Context to hold the state of the game
    {
        public bool isGaming;
    }
    public enum State // Represents the different states of the game
    {
        Game,
        Story,
    }
    Ctx ctx = new()
    {
        isGaming = false
    };
    public State currentState = State.Story;
    public static Action<State> OnStateChange;

    public void StartGame()
    {
        ctx.isGaming = true;
    }

    public void StartStory()
    {
        ctx.isGaming = false;
    }

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
            TranslateState(State.Story); // Transition back to Story state if not gaming
        }
    }
    void TranslateState(State newState)
    {
        currentState = newState;
        OnStateChange?.Invoke(newState); // Notify subscribers about the state change
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