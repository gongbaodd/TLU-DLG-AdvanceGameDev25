using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scenes.FruitNinja.Scripts
{
    public class SpawnFruitModel
    {
        public List<GameObject> fruits = new();
        public List<GameObject> bombs = new();
        public GameObject currentObj;
    }
}