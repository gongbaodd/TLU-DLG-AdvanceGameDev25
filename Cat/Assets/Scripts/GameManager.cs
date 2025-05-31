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
        // pauseButton.onClick.AddListener(() => PauseGame());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Action OnOpenStory;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Quest started");
            OnOpenStory?.Invoke();
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
