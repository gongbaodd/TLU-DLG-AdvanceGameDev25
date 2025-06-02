using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneManagerController : MonoBehaviour
{
    public static SceneManagerController Instance { get; private set; }
    [SerializeField] GameObject loading;
    void ToggleLoading(bool value) => loading.SetActive(value);
    AsyncOperationHandle previousHander;
    async Task LoadScene(AssetReference scene)
    {
        ToggleLoading(true);
        var downloadHandler = Addressables.DownloadDependenciesAsync(scene);
        await downloadHandler.Task;

        var handler = scene.LoadSceneAsync(LoadSceneMode.Single);
        await handler.Task;

        ToggleLoading(false);

        if (previousHander.IsValid())
        {
            Addressables.Release(previousHander);
        }

        previousHander = handler;
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
    [SerializeField] AssetReference FruitNinjaGameScene;
    public void GotoFruitNinjaGameScene()
    {
        _ = LoadScene(FruitNinjaGameScene);
    }
    [SerializeField] AssetReference DiabloGameScene;
    public void GotoDiabloGameScene()
    {
        _ = LoadScene(DiabloGameScene);
    }
    [SerializeField] AssetReference MainMenuScene;
    public void GotoMainMenuScene()
    {
        _ = LoadScene(MainMenuScene);
    }

    public void GotoNextLevel()
    {
        var items = Inventory.instance.GetItems();

        var hasDiablo = items.Exists(x => x.name == "Diablo Memory");
        var hasFruitNinja = items.Exists(x => x.name == "Fruit Memory");

        if (!hasFruitNinja)
        {
            GotoFruitNinjaGameScene();
            return;
        }
        if (!hasDiablo)
        {
            GotoDiabloGameScene();
            return;
        }
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
