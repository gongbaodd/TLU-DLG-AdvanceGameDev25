using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class StarterSceneController : MonoBehaviour
{
    [SerializeField] string gardenSceneAddress = "Assets/Scenes/Garden/Garden-design.unity";
    [SerializeField] string fruitNinjaSceneAddress = "Assets/Scenes/FruitNinja/FruitNinja.unity";
    [SerializeField] string diabloSceneAddress = "Assets/Scenes/Diablo/Diablo.unity";
    public void LoadGardenScene()
    {
        Addressables.LoadSceneAsync(gardenSceneAddress, LoadSceneMode.Single);
    }

    public void LoadFruitNinjaScene()
    {
        Addressables.LoadSceneAsync(fruitNinjaSceneAddress, LoadSceneMode.Single);
    }

    public void LoadDiabloScene()
    {
        Addressables.LoadSceneAsync(diabloSceneAddress, LoadSceneMode.Single);
    }
}
