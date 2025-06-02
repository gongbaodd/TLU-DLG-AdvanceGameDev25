using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // This is a singleton pattern to ensure only one instance of GameManager exists.

    //public GameObject Inventory;
    private static string playerTag = "Player";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

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
        }
        else if (other == null || !other.CompareTag(playerTag))
        {
            Debug.LogWarning("Collider is null or does not have the expected tag.");
        }
    }
}
