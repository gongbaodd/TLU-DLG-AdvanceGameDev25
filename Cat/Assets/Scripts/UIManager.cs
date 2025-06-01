using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // This is a singleton pattern to ensure only one instance of GameManager exists.
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button pauseButton;
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
        pauseButton.onClick.AddListener(PauseGame);

    }

    private void PauseGame()
    {
        if (!isPaused)
        {
            pauseButton.onClick.RemoveListener(PauseGame);
            pauseButton.onClick.AddListener(ResumeGame);
            Time.timeScale = 0f; // Pause the game
            pauseMenu.SetActive(true);
            isPaused = true;
        }

    }
    private void ResumeGame()
    {
        Time.timeScale = 1f; // Resuming the game to allow normal gameplay
        pauseMenu.SetActive(false);
        isPaused = false;
        pauseButton.onClick.RemoveListener(ResumeGame);
        pauseButton.onClick.AddListener(PauseGame);
    }
}
