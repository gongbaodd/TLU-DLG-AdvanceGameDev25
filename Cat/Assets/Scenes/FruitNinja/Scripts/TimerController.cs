using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scenes.FruitNinja.Scripts
{
    [RequireComponent(typeof(UIDocument))]
    public class TimerController : MonoBehaviour
    {
        GameObject manager;
        float timeLeft;
        Label label;

        IEnumerator TimerCoroutine()
        {
            while (timeLeft > 0)
            {
                timeLeft -= 1;

                if (timeLeft < 6) {
                    label.text = timeLeft.ToString("0");
                }

                yield return new WaitForSeconds(1f);
            }
        }



        void Start()
        {
            manager = GameObject.FindWithTag("GameController");
            if (manager == null)
            {
                throw new System.Exception("GameManager not found in the scene.");
            }

            var config = manager.GetComponent<SpawnFruitController>().Config;
            var ui = GetComponent<UIDocument>();
            var root = ui.rootVisualElement;
            label = root.Q<Label>("timerLabel");

            timeLeft = config.timeInSeconds;

            StartCoroutine(TimerCoroutine());
        }
    }
}


