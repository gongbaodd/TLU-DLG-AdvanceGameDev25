using System.Collections;
using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{

    [RequireComponent(typeof(SpawnFruitController))]
    [RequireComponent(typeof(CursorController))]
    [RequireComponent(typeof(AudioController))]
    [RequireComponent(typeof(LevelStateController))]
    public class LevelManagerController : MonoBehaviour
    {
        public static LevelManagerController Instance;
        [SerializeField] Item memoryItem;
        public float vfxTime = .6f;
        public void StartGame() {
            var stateManager = GetComponent<LevelStateController>();
            stateManager.StartGame();
        }

        public void Win()
        {
            IEnumerator WinRoutine()
            {
                var inventory = Inventory.instance;
                inventory.Add(memoryItem);
                
                GetComponent<AudioController>().PlayWinSound();

                yield return new WaitForSeconds(vfxTime);
            }

            StartCoroutine(WinRoutine());
        }

        public void Lose()
        {
            IEnumerator LoseRoutine()
            {
                GetComponent<AudioController>().PlayFailSound();
                yield return new WaitForSeconds(vfxTime);
            }

            StartCoroutine(LoseRoutine());
        }

        void Awake()
        {
            Instance = this;
        }

        void OnDestroy()
        {
            Instance = null;
        }
    }

}
