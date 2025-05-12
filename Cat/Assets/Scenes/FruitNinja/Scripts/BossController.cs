using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] GameObject boss;
        void ToggleBoss(LevelStateController.State state)
        {
            switch (state)
            {
                case LevelStateController.State.Story:
                    boss.SetActive(false);
                    break;
                case LevelStateController.State.Game:
                    boss.SetActive(true);
                    boss.GetComponent<Animator>().SetTrigger("Soccer");
                    break;
            }

        }

        void Start()
        {
            boss.SetActive(false);

            LevelStateController.OnStateChange += ToggleBoss;
        }

        void OnDestroy() {
            LevelStateController.OnStateChange -= ToggleBoss;
        }
    }
}
