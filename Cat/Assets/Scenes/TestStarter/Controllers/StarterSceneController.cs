using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class StarterSceneController : MonoBehaviour
{
    [SerializeField] string gardenSceneAddress = "Assets/Scenes/Garden/Garden.unity";
    private AsyncOperationHandle<SceneInstance> loadHandle;
    public void LoadCardenScene()
    {
        loadHandle = Addressables.LoadSceneAsync(gardenSceneAddress, LoadSceneMode.Additive);
    }

    void OnDestroy()
    {
        Addressables.UnloadSceneAsync(loadHandle);
    }
}
