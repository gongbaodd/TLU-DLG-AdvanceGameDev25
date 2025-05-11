using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public static SceneManagerController Instance { get; private set; }
    [SerializeField] GameObject loading;
    void ToggleLoading(bool value) => loading.SetActive(value);

    async Task LoadScene(AssetReference scene) {
        ToggleLoading(true);
        var handler = Addressables.LoadSceneAsync(scene, LoadSceneMode.Single);
        await handler.Task;
        ToggleLoading(false);
    }
    [SerializeField] AssetReference DebugScene;
    public void GotoDebugScene()
    {
        _ = LoadScene(DebugScene);
    }
    [SerializeField] AssetReference GardenScene;
    public void GotoGardenScene()
    {
        _ = LoadScene(GardenScene);
    }
    [Header("Fruit Ninja")]
    [SerializeField] AssetReference FruitNinjaStoryScene;
    public void GotoFruitNinjaStoryScene() 
    {
        _ = LoadScene(FruitNinjaStoryScene);
    }
    [SerializeField] AssetReference FruitNinjaGameScene;
    public void GotoFruitNinjaGameScene() 
    {
        _ = LoadScene(FruitNinjaGameScene);
    }

    void ToSingleton()
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

    void Awake()
    {
        ToSingleton();
        ToggleLoading(false);
    }
}
