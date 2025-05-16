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
    public GameObject nextButton;
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

    public enum Level
    {
        FruitNinja,
        Diablo,
        None,
    }
    Level GetNextLevel()
    {
        var items = Inventory.instance.GetItems();

        var hasDiablo = items.Exists(x => x.name == "Diablo Memory");
        var hasFruitNinja = items.Exists(x => x.name == "Fruit Memory");

        if (!hasFruitNinja) 
        {
            return Level.FruitNinja;
        }
        if (!hasDiablo) 
        {
            return Level.Diablo;
        }
        return Level.None;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Quest started");
            questDialoguebox.SetActive(true);

            var next = GetNextLevel();

            if (next != Level.None)
            {
                QuestDialogueText.text = "Hello Player";
            }
            else
            {
                QuestDialogueText.text = "You get all the items!";
                nextButton.SetActive(false);
            }

        }

    }

    public void MovetoNextLevel()
    {
        /*
        string sceneAddress = "Assets/Scenes/MainScene/MainScene.unity"; // Address of the scene in Addressables

        Debug.Log("Loading next level using Addressables...");
        Addressables.LoadSceneAsync(sceneAddress).Completed += OnSceneLoaded;
        */
        var next = GetNextLevel();
        var sceneManager = SceneManagerController.Instance.GetComponent<SceneManagerController>();
        if (next == Level.FruitNinja) 
        {
            sceneManager.GotoFruitNinjaGameScene();
        }
        else
        {
            sceneManager.GotoDiabloGameScene();
        }
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
