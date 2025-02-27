using UnityEngine;

public class DiabloController : MonoBehaviour
{
    [Header("Player")]
    private GameObject player;
    private Vector3 targetPos;

    [SerializeField] private float speed = 0.8f;
    [SerializeField] private float movementThreshold = 0.01f;

    private void InitPlayer()
    {
        player = GameObject.Find("Player");

        if (player == null)
        {
            Debug.LogError("Player not found");
        }

        player.GetComponent<CatController>().Stand();
        targetPos = player.transform.position;
    }
    private void RotatePlayer(Vector3 position)
    {
        targetPos = new Vector3(position.x, player.transform.position.y, position.z);
        player.transform.LookAt(targetPos);
    }

    private void MovePlayer()
    {

        if (Vector3.Distance(player.transform.position, targetPos) > movementThreshold)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, Time.deltaTime * speed);
            player.GetComponent<CatController>().Walk();
        }
        else
        {
            player.GetComponent<CatController>().Stand();
        }
    }


    [Header("Camera")]
    private GameObject mainCamera;
    [SerializeField] private Vector3 cameraRelativePos = new Vector3(0, 3, -1);
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
                RotatePlayer(hit.point);
            }
        }

        MovePlayer();
    }
}
