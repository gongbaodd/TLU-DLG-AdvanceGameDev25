using UnityEngine;

public class DiabloController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;
    private Vector3? targetPos;

    [SerializeField] private float speed = 0.8f;
    [SerializeField] private float movementThreshold = 0.01f;

    private void InitPlayer()
    {
        if (player == null)
        {
            Debug.LogError("Player not found");
        }

        player.GetComponent<Animator>().CrossFade("standing", 0f);
    }
    private void RotatePlayer(Vector3 position)
    {
        targetPos = new Vector3(position.x, player.transform.position.y, position.z);
        player.transform.LookAt(targetPos ?? Vector3.zero);
    }

    private void MovePlayer()
    {
        if (targetPos == null) return;

        if (Vector3.Distance(player.transform.position, targetPos ?? Vector3.zero) > movementThreshold)
        {
            var agent = player.GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.SetDestination(targetPos ?? Vector3.zero);
            player.GetComponent<CatController>().Walk();
        }
        else
        {
            player.GetComponent<CatController>().Stand();
        }
    }


    [Header("Camera")]
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Vector3 cameraRelativePos = new Vector3(0, 3, -1);
    private void InitCamera()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found");
        }
    }

    private void UpdateCamera()
    {

        if (player == null )
        {
            Debug.LogError("WebGL: Player is NULL!");
            return;
        }

        if (mainCamera == null)
        {
            Debug.LogError("WebGL: Camera is NULL!");
            return;
        }

        Debug.Log("WebGL: Updating Camera...");
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

    void LateUpdate()
    {
        UpdateCamera();
    }
}
