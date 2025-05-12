
using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        AudioSource sound;
        [SerializeField] AudioClip boxBiteSound;
        public void PlayBoxBiteSound() => sound.PlayOneShot(boxBiteSound);
        [SerializeField] AudioClip enemyPunchSound;
        public void PlayEnemyPunchSound() => sound.PlayOneShot(enemyPunchSound);
        [SerializeField] AudioClip winSound;
        public void PlayWinSound() => sound.PlayOneShot(winSound);
        [SerializeField] AudioClip loseSound;
        public void PlayLoseSound() => sound.PlayOneShot(loseSound);

        void Awake()
        {
            sound = GetComponent<AudioSource>();
        }
    }
}