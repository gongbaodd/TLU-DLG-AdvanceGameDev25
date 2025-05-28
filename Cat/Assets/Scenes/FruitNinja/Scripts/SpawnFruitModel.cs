using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scenes.FruitNinja.Scripts
{
    class SpawnFruitModel
    {
        public List<GameObject> fruits = new();
        public List<GameObject> bombs = new();
        public GameObject currentObj;
    }
}