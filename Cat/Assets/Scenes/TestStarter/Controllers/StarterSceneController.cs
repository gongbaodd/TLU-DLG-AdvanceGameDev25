using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class StarterSceneController : MonoBehaviour
{
    [SerializeField] string gardenSceneAddress = "Assets/Scenes/Garden/Garden.unity";
    [SerializeField] string fruitNinjaSceneAddress = "Assets/Scenes/FruitNinja/FruitNinja.unity";
    public void LoadCardenScene()
    {
        Addressables.LoadSceneAsync(gardenSceneAddress, LoadSceneMode.Single);
    }

    public void LoadFruitNinjaScene() {
        Addressables.LoadSceneAsync(fruitNinjaSceneAddress, LoadSceneMode.Single);
    }
}
