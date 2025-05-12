using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Make sure to include this for scene management

public class GameManager : MonoBehaviour
{
    public GameObject questDialoguebox;
    public TMP_Text QuestDialogueText;
    public Button nextLevelButton;

    private static String playerTag = "Player";


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Quest started");
            questDialoguebox.SetActive(true);
            QuestDialogueText.text = "Hello Player";
        }

    }

    public void MovetoNextLevel()
    {
        /*
        string sceneAddress = "Assets/Scenes/MainScene/MainScene.unity"; // Address of the scene in Addressables

        Debug.Log("Loading next level using Addressables...");
        Addressables.LoadSceneAsync(sceneAddress).Completed += OnSceneLoaded;
        */
        SceneManagerController.Instance.GotoNextLevel();
    }

    // Callback to handle post-load actions
    private void OnSceneLoaded(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Successfully loaded the next level.");
        }
        else
        {
            Debug.LogError("Failed to load the next level.");
        }
    }
}
