using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;


public class StarterSceneController : MonoBehaviour
{
    [SerializeField] AssetReference gardenScene;
    [SerializeField] AssetReference fruitNinjaScene;
    [SerializeField] AssetReference diabloScene;
    [SerializeField] AssetReference inventoryScene;
    
    private AsyncOperationHandle handle;
    public void LoadGardenScene()
    {
        handle = gardenScene.LoadSceneAsync(LoadSceneMode.Single);
    }

    public void LoadFruitNinjaScene()
    {
        handle = fruitNinjaScene.LoadSceneAsync(LoadSceneMode.Single);
    }

    public void LoadDiabloScene()
    {
        handle = diabloScene.LoadSceneAsync(LoadSceneMode.Single);
    }

    public void LoadInventoryScene()
    {
        handle = inventoryScene.LoadSceneAsync(LoadSceneMode.Single);
    }

    void OnDestroy()
    {
        // TODO: destory logic
        print("Destroying StarterSceneController");
    }
}
