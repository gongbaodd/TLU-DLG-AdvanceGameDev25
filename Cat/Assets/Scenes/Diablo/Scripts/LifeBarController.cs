using UnityEngine;
namespace Assets.Scenes.Diablo.Scripts
{
    public class LifeBarController : MonoBehaviour
    {
        [SerializeField] GameObject lifebar;

        void Start()
        {
            Instantiate(lifebar, transform.position + Vector3.up * 2, Quaternion.identity, transform);   
        }
    }
}