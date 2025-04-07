using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{
    [RequireComponent(typeof(GetManager))]
    public class BossController : MonoBehaviour
    {
        void ThrowMemoryPiece()
        {
            // Logic to spawn the boss
        }
        void Start()
        {
            TimerController.OnTimerEnd += ThrowMemoryPiece;
        }

        void OnDestroy()
        {
            TimerController.OnTimerEnd -= ThrowMemoryPiece;
        }
    }
}

