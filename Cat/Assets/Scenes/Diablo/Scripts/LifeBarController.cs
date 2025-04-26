using UnityEngine;
namespace Assets.Scenes.Diablo.Scripts
{
    public class LifeBarController : MonoBehaviour
    {
        [SerializeField] GameObject lifebar;

        GameObject myLifebar;

        GameObject myIndicator;

        float fullLife = 100f;

        float currentLife = 100f;

        public void Attacked(float attackValue) {
            currentLife -= attackValue;
        }

        void Start()
        {
             myLifebar = Instantiate(lifebar, transform.position + Vector3.up * 2, Quaternion.identity, transform);
             myIndicator = myLifebar.transform.Find("indicator").gameObject;
        }

        void Update()
        {
            if (myLifebar != null) {
                myLifebar.transform.LookAt(Camera.main.transform.position);
                myLifebar.transform.Rotate(0, 180f, 0);
                myIndicator.transform.localScale = new Vector3(currentLife / fullLife, 1, 1);
            }

            if (currentLife <= 0) {
                var manager = DiabloController.gameManager.GetComponent<DiabloController>();
                manager.Lose();
            }
        }
    }
}