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

    SceneManagerController manager;
    
    public void LoadGardenScene()
    {
        manager.GotoGardenScene();
    }

    public void LoadFruitNinjaScene()
    {
        manager.GotoFruitNinjaGameScene();
    }

    public void LoadDiabloScene()
    {
        manager.GotoDiabloGameScene();
    }

    void Start()
    {
        manager = SceneManagerController.Instance.GetComponent<SceneManagerController>();
    }
}
