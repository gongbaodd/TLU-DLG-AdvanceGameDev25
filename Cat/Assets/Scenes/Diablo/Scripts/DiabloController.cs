using UnityEngine;

public class DiabloController : MonoBehaviour
{
    [Header("Player")]
    private GameObject player;

    private void InitPlayer()
    {
        player = GameObject.Find("Player");

        if (player == null)
        {
            Debug.LogError("Player not found");
        }

        player.GetComponent<CatController>().Stand();
    }

    [Header("Camera")]
    private GameObject mainCamera;
    [SerializeField] private Vector3 cameraRelativePos = new Vector3(0, 3, -2);

    private void InitCamera()
    {
        mainCamera = GameObject.Find("Main Camera");

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found");
        }
    }

    private void UpdateCamera()
    {
        mainCamera.transform.position = player.transform.position + cameraRelativePos;
        mainCamera.transform.LookAt(player.transform);
    }

    /** LifeCycle **/
    void Start()
    {
        InitPlayer();
        InitCamera();
    }

    void LateUpdate()
    {
        UpdateCamera();
    }
}
