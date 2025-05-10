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
        AudioSource player;

        public void Win()
        {
            throw new System.NotImplementedException("Need to addItem to Inventory! Wait the Inventory to be implemented!");
        }

        public void Lose()
        {
            print("Lose");
        }
        [SerializeField] AudioClip spawnSound;
        public void PlaySpawnSound() => player.PlayOneShot(spawnSound);
        [SerializeField] AudioClip poofSound;
        public void PlayPoofSound() => player.PlayOneShot(poofSound);
        [SerializeField] AudioClip boomSound;
        public void PlayBoomSound() => player.PlayOneShot(boomSound);
        [SerializeField] AudioClip outBorderSound;
        public void PlayOutBorderSound() => player.PlayOneShot(outBorderSound);

        void Awake()
        {
            Manager = gameObject;
            player = GetComponent<AudioSource>();
        }
        void OnDestroy()
        {
            Manager = null;
        }
    }

}
