using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;


public class StarterSceneController : MonoBehaviour
{
    [SerializeField] AssetReference gardenScene;    
    [SerializeField] AssetReference fruitNinjaScene;
    [SerializeField] AssetReference diabloScene;
    [SerializeField] AssetReference inventoryScene;
    public void LoadGardenScene()
    {
        gardenScene.LoadSceneAsync(LoadSceneMode.Single);
    }

    public void LoadFruitNinjaScene()
    {
        fruitNinjaScene.LoadSceneAsync(LoadSceneMode.Single);
    }

    public void LoadDiabloScene()
    {
        diabloScene.LoadSceneAsync(LoadSceneMode.Single);
    }

    public void LoadInventoryScene()
    {
        inventoryScene.LoadSceneAsync(LoadSceneMode.Single);
    }
}
