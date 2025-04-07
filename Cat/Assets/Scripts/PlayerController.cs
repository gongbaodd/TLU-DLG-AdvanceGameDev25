using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems; // For event system handling


public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;

    public float interactRange = 100f; // Range for interaction
    public LayerMask groundMask;
    public LayerMask interactableMask; // Mask for interactable objects

    public float movementSpeed = 2f;

    private NavMeshAgent catAgent;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private Rigidbody rb;

    private Interactable currentFocus; // Current interactable object

    void Start()
    {
        catAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; // Ignore clicks if over UI elements
        }

        // Handle movement and interaction
        if (Input.GetMouseButtonDown(0))
        {
            //MoveToClickPoint();
            HandleMouseClick(); // Call the method to handle mouse click

        }


    }

    void OnMouseClicked()
    {
        // Get the click position on screen
        Vector3 clickPosition = Input.mousePosition;

        // Create a ray starting at click point on screen and moves along the camera perspective
        Ray clickRay = mainCamera.ScreenPointToRay(clickPosition);

        // Declare a variable to store the Raycast hit information
        RaycastHit hit;

        // Physics.Raycast returns a bool (true/false) whether it hit a collider or not
        // Interaction mask allows us to filter what objects the ray should register
        if (Physics.Raycast(clickRay, out hit, 100f, groundMask))
        {
            print("Something clicked");

            // Do other logic with the object we clicked on
            //catAgent.SetDestination(hit.point);
            targetPosition = hit.point;
            isMoving = true;
            //catAnim.SetBool("isMoving", true);

        }
        else
        {
            print("Nothing clicked!");
        }


    }

    void HandleMouseClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 1. Try hitting an interactable first
        if (Physics.Raycast(ray, out hit, interactRange, interactableMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                SetFocus(interactable);
                catAgent.SetDestination(interactable.interactionTransform != null
                    ? interactable.interactionTransform.position
                    : interactable.transform.position);
                return;
            }
        }

        // 2. Otherwise, move to the ground
        if (Physics.Raycast(ray, out hit, interactRange, groundMask))
        {
            RemoveFocus();
            catAgent.SetDestination(hit.point);
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != currentFocus)
        {
            if (currentFocus != null)
                currentFocus.OnDefocused();

            currentFocus = newFocus;
            currentFocus.OnFocused(transform);
        }
    }

    void RemoveFocus()
    {
        if (currentFocus != null)
        {
            currentFocus.OnDefocused();
            currentFocus = null;
        }
    }

    void MoveToClickPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Only move if we click on the ground
        if (Physics.Raycast(ray, out hit, 100f, groundMask))
        {
            catAgent.SetDestination(hit.point); // Move to clicked position
        }
    }

}