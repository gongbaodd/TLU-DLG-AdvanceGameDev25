using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scenes.Diablo.Prefabs.Level.Scripts
{
    public class ObjectLoaders : MonoBehaviour
    {
        [SerializeField] List<AssetReference> prefabs;
        void Awake()
        {
            foreach (var prefab in prefabs)
            {
                prefab.InstantiateAsync(Vector3.zero, Quaternion.identity, gameObject.transform);
            }
        }
    }
}
