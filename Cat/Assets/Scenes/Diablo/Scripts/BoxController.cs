using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class BoxController : MonoBehaviour
    {
        public static readonly string BOXTAG = "DiabloBox";
        [SerializeField] BoxConfig boxConfig;

        void OnFindMemory()
        {
            var manager = DiabloController.gameManager;

            if (manager)
            {
                manager.GetComponent<DiabloController>().Win();
            }
        }

        void OnTriggerMonster() {
            print("run!");
        }

        void OnTriggerEnter(Collider other)
        {
            var player = DiabloController.player;
            var playerInteractablesController = player.GetComponent<PlayerInteractablesController>();

            if (other.CompareTag("Player") && playerInteractablesController.CompareInteractable(gameObject))
            {
                switch (boxConfig.content)
                {
                    case BoxContent.None:
                        break;
                    case BoxContent.Memory:
                        OnFindMemory();
                        break;
                    case BoxContent.Health:
                        break;
                    case BoxContent.Monster:
                        OnTriggerMonster();
                        break;
                    default:
                        throw new System.Exception("Invalid box content");
                }
            }
        }
    }



}


