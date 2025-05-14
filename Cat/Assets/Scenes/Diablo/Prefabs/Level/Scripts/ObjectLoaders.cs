using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scenes.Diablo.Prefabs.Level.Scripts
{
    public class ObjectLoaders : MonoBehaviour
    {
        [SerializeField] List<GameObject> prefabList;
        List<GameObject> instances = new();
        void Awake()
        {
            foreach (var prefab in prefabList)
            {
                var ins = Instantiate(prefab, Vector3.zero, Quaternion.identity, gameObject.transform);
                instances.Add(ins);
            }
        }

        void OnDestroy()
        {
            foreach(var ins in instances) Destroy(ins);
        }
    }
}
