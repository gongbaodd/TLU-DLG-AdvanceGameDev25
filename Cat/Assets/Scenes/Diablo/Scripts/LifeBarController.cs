using UnityEngine;
namespace Assets.Scenes.Diablo.Scripts
{
    public class LifeBarController : MonoBehaviour
    {
        [SerializeField] GameObject lifebar;

        GameObject myLifebar;

        void Start()
        {
             myLifebar = Instantiate(lifebar, transform.position + Vector3.up * 2, Quaternion.identity, transform);   
        }

        void Update()
        {
            if (myLifebar != null) {
                myLifebar.transform.LookAt(Camera.main.transform.position);
                myLifebar.transform.Rotate(0, 180f, 0);
            }
        }
    }
}