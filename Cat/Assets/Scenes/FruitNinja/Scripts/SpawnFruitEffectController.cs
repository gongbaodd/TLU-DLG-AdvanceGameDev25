using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{
    [RequireComponent(typeof(SpawnFruitController))]
    [RequireComponent(typeof(AudioController))]
    public class SpawnFruitEffectController : MonoBehaviour
    {
        private AudioController audioController;
        private GameConfig config;

        public void PlayPoof(Vector2 fruitPos)
        {
            var poofPrefab = config.poofPrefab;
            var poof = Instantiate(poofPrefab, new Vector3(fruitPos.x, fruitPos.y, 0), Quaternion.identity);
            poof.GetComponent<ParticleSystem>().Play();
            audioController.PlayPoofSound();
            Destroy(poof, 1f);
        }

        public void PlayBoom(Vector2 boomPos)
        {
            var boomPrefab = config.boomPrefab;
            var boom = Instantiate(boomPrefab, new Vector3(boomPos.x, boomPos.y, 0), Quaternion.identity);
            boom.GetComponent<ParticleSystem>().Play();
            audioController.PlayBoomSound();
            Destroy(boom, 1f);
        }

        void Start()
        {
            config = GetComponent<SpawnFruitController>().Config;
            audioController = GetComponent<AudioController>();
        }
    }
} 