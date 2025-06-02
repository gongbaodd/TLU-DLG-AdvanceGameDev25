using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{
    [RequireComponent(typeof(SpawnFruitController))]
    [RequireComponent(typeof(LevelManagerController))]
    public class SpawnOutBorderController : MonoBehaviour
    {
        [SerializeField] private GameObject dropOutMark;
        [SerializeField] private GameObject leftBorder;
        [SerializeField] private GameObject rightBorder;
        [SerializeField] private GameObject bottomBorder;

        float LeftBorder => leftBorder.transform.position.x;
        float RightBorder => rightBorder.transform.position.x;
        float BottomBorder => bottomBorder.transform.position.y;

        enum BorderType
        {
            Left,
            Right,
            Bottom,
        }

        private SpawnFruitController spawnFruitController;
        private LevelManagerController gameCtrl;
        private AudioController audioController;

        public void CheckFruitsOutOfBorder(List<SpawnFruitModel> activeFruits)
        {
            for (int i = activeFruits.Count - 1; i >= 0; i--)
            {
                var fruit = activeFruits[i];
                if (!fruit.currentObj.activeInHierarchy)
                {
                    continue;
                }

                var pos = fruit.currentObj.transform.position;
                BorderType? outBorder = null; // Initialize outBorder as null to check if the fruit is out of any border
                if (pos.x < LeftBorder)
                {
                    outBorder = BorderType.Left;
                }
                else if (pos.x > RightBorder)
                {
                    outBorder = BorderType.Right;
                }
                else if (pos.y < BottomBorder)
                {
                    outBorder = BorderType.Bottom;
                }
                if (outBorder.HasValue)
                {
                    InitOutBoundaryMark(outBorder.Value, pos);
                    activeFruits.RemoveAt(i);
                    spawnFruitController.ReleaseFruit(fruit);
                    audioController.PlayOutBorderSound();
                    gameCtrl.Lose();
                }
            }
        }

        // Initializes the out-of-boundary mark at the specified position based on the border type.
        private void InitOutBoundaryMark(BorderType border, Vector3 position)
        {
            Vector3 pos = position;
            switch (border)
            {
                case BorderType.Left:
                    pos = new Vector3(LeftBorder + 4f, position.y, 0);
                    break;
                case BorderType.Right:
                    pos = new Vector3(RightBorder - 4f, position.y, 0);
                    break;
                case BorderType.Bottom:
                    pos = new Vector3(position.x, BottomBorder + 4f, 0);
                    break;
            }
            Instantiate(dropOutMark, pos, Quaternion.identity); // Instantiate the drop-out mark at the calculated position
        }

        void Awake()
        {
            spawnFruitController = GetComponent<SpawnFruitController>();
            gameCtrl = GetComponent<LevelManagerController>();
            audioController = gameCtrl.GetComponent<AudioController>();
        }

    }
}