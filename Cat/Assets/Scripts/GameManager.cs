using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Make sure to include this for scene management

public class GameManager : MonoBehaviour
{
    public GameObject dialogController;
    public GameObject questDialoguebox;
    public GameObject nextButton;
    public TMP_Text QuestDialogueText;
    public Button nextLevelButton;
    public GameObject pauseMenu;
    public Button pauseButton;

    public static GameManager Instance { get; private set; }


    private static String playerTag = "Player";
    private bool isPaused = false;

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

    void Start()
    {
        pauseButton.onClick.AddListener(() => PauseGame());

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
            dialogController.SetActive(true);

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

    private void PauseGame()
    {
        if (!isPaused)
        {
            pauseButton.onClick.RemoveListener(() => PauseGame());
            pauseButton.onClick.AddListener(() => ResumeGame());
            Time.timeScale = 0f; // Pause the game
            pauseMenu.SetActive(true);
            isPaused = true;
        }

    }
    private void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        pauseMenu.SetActive(false);
        isPaused = false;
        pauseButton.onClick.RemoveListener(() => ResumeGame());
        pauseButton.onClick.AddListener(() => PauseGame());
    }
}
