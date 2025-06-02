using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // This is a singleton pattern to ensure only one instance of GameManager exists.
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button backtoMenuButton;
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
        controlsButton.onClick.AddListener(OpenControlsMenu);
        backButton.onClick.AddListener(CloseControlsMenu);
        backtoMenuButton.onClick.AddListener(BackToMenu);
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

    private void OpenControlsMenu()
    {
        // Logic to open the controls menu
        controlsMenu.SetActive(true);
        pauseMenu.SetActive(false);

    }
    private void CloseControlsMenu()
    {
        // Logic to close the controls menu
        controlsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void BackToMenu()
    {
        var sceneManager = SceneManagerController.Instance.GetComponent<SceneManagerController>();
        pauseMenu.SetActive(false);
        sceneManager.GotoMainMenuScene();
    }

    public void ShowWinPanel()
    {
        // Logic to show the win panel
        Debug.Log("You won the game!");
        winPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game when showing the win panel
    }

}
