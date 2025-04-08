using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{

    [CreateAssetMenu(menuName = "Diablo/BoxConfig")]
    public class BoxConfig : ScriptableObject
    {
        public bool isMemoryStored = false;

    }
    public class BoxController : MonoBehaviour
    {
        [SerializeField] BoxConfig boxConfig;
        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player")) {
                if (boxConfig.isMemoryStored) {
                    var manager = DiabloController.gameManager;

                    if (manager) {
                        manager.GetComponent<DiabloController>().Win();
                    }
                }
            }
        }
    }



}


