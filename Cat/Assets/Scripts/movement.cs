using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class movement : MonoBehaviour
{
    public Camera mainCamera;
    public Animator catAnim;

    public LayerMask interactMask;
    public GameObject cat;
    public float movementSpeed = 2f;
    //public float stoppingDistance = 0.1f;

    //private NavMeshAgent catAgent;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        //catAgent = cat.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClicked();
        }

        // Check if the cat has reached its destination
        // if (!catAgent.pathPending && catAgent.remainingDistance <= stoppingDistance)
        // {
        //     catAnim.SetBool("isMoving", false);
        // }

        if (isMoving)
        {

            cat.transform.position = Vector3.Lerp(cat.transform.position, targetPosition, movementSpeed * Time.deltaTime);

            if (Vector3.Distance(cat.transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                catAnim.SetBool("isMoving", false);
            }
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
        if (Physics.Raycast(clickRay, out hit, 100f, interactMask))
        {
            print("Something clicked");

            // Do other logic with the object we clicked on
            //catAgent.SetDestination(hit.point);
            targetPosition = hit.point;
            isMoving = true;
            catAnim.SetBool("isMoving", true);

        }
        else
        {
            print("Nothing clicked!");
        }


        // Project the click screen position to in-game world position
        Vector3 clickOrigin = mainCamera.ScreenToWorldPoint(new Vector3(clickPosition.x, clickPosition.y, 0f));

        // Draw editor debugger to see ray in action
        Debug.DrawRay(clickOrigin, clickRay.direction * 100f, Color.yellow, 0.5f);
    }
}