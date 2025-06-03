using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneManagerController : MonoBehaviour
{
    public static SceneManagerController Instance { get; private set; }
    [SerializeField] GameObject loading;
    AssetReference currentSceneReference;
    void ToggleLoading(bool value) => loading.SetActive(value);
    AsyncOperationHandle previousHandler;
    async Task LoadScene(AssetReference scene)
    {
        ToggleLoading(true);
        var downloadHandler = Addressables.DownloadDependenciesAsync(scene);
        await downloadHandler.Task;

        // Only unload if there is more than one scene loaded
        if (previousHandler.IsValid() && SceneManager.sceneCount > 1)
        {
            await Addressables.UnloadSceneAsync(previousHandler, true).Task;
        }

        // if (scene.OperationHandle.IsValid() && scene.OperationHandle.Status == AsyncOperationStatus.Succeeded)
        // {
        //     Debug.LogWarning("Scene is already loaded.");
        //     ToggleLoading(false);
        //     return;
        // }
        var handler = scene.LoadSceneAsync(LoadSceneMode.Single);
        await handler.Task;

        ToggleLoading(false);

        // if (previousHandler.IsValid())
        // {
        //     Addressables.Release(previousHandler);
        // }

        previousHandler = handler;
        currentSceneReference = scene;
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

    public void GotoNextLevel()
    {
        var items = Inventory.instance.GetItems();

        var hasDiablo = items.Exists(x => x.name == "Diablo Memory");
        var hasFruitNinja = items.Exists(x => x.name == "Fruit Memory");

        if (!hasFruitNinja)
        {
            GotoGardenScene();
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
