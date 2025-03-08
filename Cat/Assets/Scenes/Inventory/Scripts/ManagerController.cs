using UnityEngine;

namespace Assets.Scenes.Inventory.Scripts
{
    [RequireComponent(typeof(UIViewModel))]
    public class ManagerController : MonoBehaviour
    {
        #region Singleton
        public static ManagerController Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
        #endregion
        
    }

}
