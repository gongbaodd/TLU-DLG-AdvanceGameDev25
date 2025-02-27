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

    [Header("Mouse")]
    private Vector3 mousePos;
    [SerializeField] private GameObject plane;
    private void InitCursor()
    {
        if (plane == null)
        {
            Debug.LogError("Plane not found");
        }
    }

    /** LifeCycle **/
    void Start()
    {
        InitPlayer();
        InitCamera();
        InitCursor();
    }

    void LateUpdate()
    {
        UpdateCamera();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // player.GetComponent<CatController>().Move(hit.point);

                Vector3 rotation = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);                    

                player.transform.position = hit.point + new Vector3(0, 1f, 0);
            }
        }
        
    }
}
