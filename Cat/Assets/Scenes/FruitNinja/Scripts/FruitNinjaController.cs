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
        [SerializeField] AudioClip spawnSound;

        public void Win()
        {
            throw new System.NotImplementedException("Need to addItem to Inventory! Wait the Inventory to be implemented!");
        }

        public void Lose()
        {
            print("Lose");
        }

        public void PlaySpawnSound() => player.PlayOneShot(spawnSound);
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
