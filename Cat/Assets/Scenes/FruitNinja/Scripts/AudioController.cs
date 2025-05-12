
using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour
    {
        AudioSource soundPlayer;
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
            soundPlayer = GetComponent<AudioSource>();
        }
    }
}