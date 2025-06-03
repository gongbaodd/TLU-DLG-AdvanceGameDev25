using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // This is a singleton pattern to ensure only one instance of GameManager exists.
    private static string playerTag = "Player";

    void Start()
    {
        questStarted = false; // Initialize questStarted to false at the start of the game.
    }

    // This action is invoked when the player enters a trigger zone to start a quest.
    public static Action OnOpenStory;
    private bool questStarted = false;

    void OnTriggerEnter(Collider other)
    {
        if (!questStarted && other != null && other.CompareTag(playerTag))
        {
            questStarted = true;
            Debug.Log("Quest started");
            // Trigger the OnOpenStory action, which starts the quest dialogue or story sequence.
            OnOpenStory?.Invoke();
            Debug.Log("OnOpenStory action invoked.");
        }
        else if (other == null || !other.CompareTag(playerTag))
        {
            Debug.LogWarning("Collider is null or does not have the expected tag.");
        }
    }
}
