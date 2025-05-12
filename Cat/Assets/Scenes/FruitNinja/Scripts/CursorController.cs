using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scenes.FruitNinja.Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    public class CursorController : MonoBehaviour
    {
        [SerializeField] GameObject CatPaw;
        [SerializeField] float mousePosZ = 10f;
        [SerializeField] float minPointDistance = 0.1f;
        LineRenderer lineRenderer;
        readonly List<Vector3> points = new();
        LevelManagerController manager;
        bool isDrawing = false;
        public bool IsDrawing => isDrawing;

        void StartDrawing()
        {
            lineRenderer.enabled = true;
            isDrawing = true;
            points.Clear();
            AddPoint(GetMouseWorldPosition());
        }

        void ContinueDrawing()
        {
            Vector3 newPos = GetMouseWorldPosition();
            if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], newPos) > minPointDistance)
            {
                AddPoint(newPos);
            }
        }

        void StopDrawing()
        {
            isDrawing = false;
            lineRenderer.enabled = false;
        }

        void AddPoint(Vector3 point)
        {
            points.Add(point);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }

        Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mousePosZ;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }

        void LockMouse()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            CatPaw.SetActive(true);
        }

        void UnlockMouse()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        void UpdateCatPawPos()
        {
            CatPaw.transform.position = GetMouseWorldPosition();
        }

        void ToggleCursor(LevelStateController.State state) {
            switch (state) {
                case LevelStateController.State.Story:
                    CatPaw.SetActive(false);
                    UnlockMouse();
                    break;
                case LevelStateController.State.Game:
                    LockMouse();
                    break;
            }
        }

        void Start()
        {
            manager = LevelManagerController.Instance;
            lineRenderer = GetComponent<LineRenderer>();
            UnlockMouse();
            LevelStateController.OnStateChange += ToggleCursor;
        }

        private float mouseSpeed;
        private Vector3 lastMousePosition;
        [SerializeField] float drawingSpeedThreshold = 50f;

        void Update()
        {
            var currentState = manager.GetComponent<LevelStateController>().currentState;
            if (currentState != LevelStateController.State.Game)
            {
                return;
            }

            Vector3 currentMousePosition = Input.mousePosition;
            mouseSpeed = (currentMousePosition - lastMousePosition).magnitude / Time.unscaledDeltaTime;
            lastMousePosition = currentMousePosition;

            if (!isDrawing && mouseSpeed > drawingSpeedThreshold)
            {
                StartDrawing();
            }
            else if (isDrawing && mouseSpeed < drawingSpeedThreshold * 0.5f)
            {
                StopDrawing();
            }

            if (isDrawing)
            {
                ContinueDrawing();
            }


            UpdateCatPawPos();
        }

        void OnDestroy()
        {
            UnlockMouse();
            LevelStateController.OnStateChange -= ToggleCursor;
        }

    }
}