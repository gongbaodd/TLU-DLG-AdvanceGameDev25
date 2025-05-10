using System.Collections;
using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    [RequireComponent(typeof(BoxCollider), typeof(BoxStateController))]
    [RequireComponent(typeof(CursorLabelController))]
    public class BoxController : MonoBehaviour
    {
        public static readonly string BOXTAG = "DiabloBox";

        public BoxConfig boxConfig;

        void OnTriggerEnter(Collider other)
        {
            var stateController = GetComponent<BoxStateController>();
            var currentState = stateController.currentState;

            if (other.CompareTag("Player") && currentState == BoxStateController.BoxState.WaitInteraction)
            {
                switch (boxConfig.content)
                {
                    case BoxContent.None:
                        break;
                    case BoxContent.Memory:
                    case BoxContent.Health:
                    case BoxContent.Monster:
                        stateController.PlayerCome();
                        break;
                    default:
                        throw new System.Exception("Invalid box content");
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var stateController = GetComponent<BoxStateController>();
                stateController.PlayerLeave();
            }
        }
    }



}


