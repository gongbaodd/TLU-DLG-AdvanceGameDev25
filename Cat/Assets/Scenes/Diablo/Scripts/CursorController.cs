using UnityEngine;

namespace Assets.Scenes.Diablo.Scripts
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private GameObject plane;

        public Vector3? HitTest() {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                return hit.point;
            }
            return null;
        }

        void Start()
        {
            if (plane == null)
            {
                Debug.LogError("Plane not found");
            }
        }

    }
}


