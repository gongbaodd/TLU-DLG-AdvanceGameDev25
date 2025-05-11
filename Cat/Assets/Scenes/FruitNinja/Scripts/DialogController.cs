using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] GameObject god;
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;

    void Awake()
    {
        god.SetActive(false);
        player.SetActive(false);
        boss.SetActive(false);
    }
    void Start()
    {
        
    }
}
