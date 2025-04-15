using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class BoxController : MonoBehaviour
    {
        [SerializeField] BoxConfig boxConfig;
        void Awake()
        {
            
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (boxConfig.isMemoryStored)
                {
                    var manager = DiabloController.gameManager;

                    if (manager)
                    {
                        manager.GetComponent<DiabloController>().Win();
                    }
                }
            }
        }
    }



}


