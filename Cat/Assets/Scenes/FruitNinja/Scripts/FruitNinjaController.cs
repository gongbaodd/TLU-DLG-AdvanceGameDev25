using System.Collections;
using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{

    [RequireComponent(
        typeof(SpawnFruitController),
        typeof(CursorController)
    )]
    public class FruitNinjaController : MonoBehaviour
    {
        public static GameObject Manager;
        AudioSource soundPlayer;
        [SerializeField] Item memoryItem;
        public float vfxTime = .6f;
        [SerializeField] GameObject boss;

        public void Win()
        {
            IEnumerator WinRoutine() {
                boss.SetActive(false);

                var inventory = Inventory.instance;
                inventory.Add(memoryItem);

                yield return new WaitForSeconds(vfxTime);
                PlayWinSound();
            }

            StartCoroutine(WinRoutine());
        }

        public void Lose()
        {
            IEnumerator LoseRoutine() {
                yield return new WaitForSeconds(vfxTime);
                PlayFailSound();
            }

            StartCoroutine(LoseRoutine());
        }
        [SerializeField] AudioClip spawnSound;
        public void PlaySpawnSound() => soundPlayer.PlayOneShot(spawnSound);
        [SerializeField] AudioClip poofSound;
        public void PlayPoofSound() => soundPlayer.PlayOneShot(poofSound);
        [SerializeField] AudioClip boomSound;
        public void PlayBoomSound() => soundPlayer.PlayOneShot(boomSound);
        [SerializeField] AudioClip outBorderSound;
        public void PlayOutBorderSound() => soundPlayer.PlayOneShot(outBorderSound);
        [SerializeField] AudioClip failSound;
        public void PlayFailSound() => soundPlayer.PlayOneShot(failSound);
        [SerializeField] AudioClip winSound;
        public void PlayWinSound() => soundPlayer.PlayOneShot(winSound);

        void Awake()
        {
            Manager = gameObject;
            soundPlayer = GetComponent<AudioSource>();
        }
        void OnDestroy()
        {
            Manager = null;
        }
    }

}
