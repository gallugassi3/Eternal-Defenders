using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 levelCenterPoint;
    [SerializeField] private float maxDistanceFromCenter;

    [Header("Movement Details")]
    [SerializeField] private float movementSpeed = 120;
    [SerializeField] private float mouseMovementSpeed = 5;

    [Header("Edge Movement Details")]
    [SerializeField] private float edgeMovementSpeed = 10;
    [SerializeField] private float edgeTresHold = 10;
    private float screenWidth;
    private float screenHeight;

    [Header("Rotation Details")]
    [SerializeField] private Transform focusPoint;
    [SerializeField] private float maxFocusPointDistance = 15;
    [Space]
    [SerializeField] private float rotationSpeed = 200;
    [Space]
    private float pitch;
    [SerializeField] private float minPitch = 5f;
    [SerializeField] private float maxPitch = 85f;

    [Header("Zoom Details")]
    [SerializeField] private float zoomSpeed = 10;
    [SerializeField] private float minZoom = 3;
    [SerializeField] private float maxZoom = 15;

    private float smoothTime = .1f;
    private Vector3 movementVelocity = Vector3.zero;
    private Vector3 mouseMovementVelocity = Vector3.zero;
    private Vector3 edgeMovementVelocity = Vector3.zero;
    private Vector3 zoomVelocity = Vector3.zero;
    private Vector3 lastMousePosition;

    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
    {
        HandleRotation();
        HandleZoom();
        //HandleEdgeMovement(); // Can use it but i didnt like the way it works.
        HandleMouseMovement();
        HandleMovement();

        focusPoint.position = transform.position + (transform.forward * GetFocusPointDistance());
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 zoomDirection = transform.forward * scroll * zoomSpeed;
        Vector3 targetPosition = transform.position + zoomDirection;

        if (transform.position.y < minZoom && scroll > 0)
        {
            return;
        }

        if (transform.position.y > maxZoom && scroll < 0)
        {
            return;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref zoomVelocity, smoothTime);
    }

    private float GetFocusPointDistance()
    {
        if(Physics.Raycast(transform.position , transform.forward , out RaycastHit hit , maxFocusPointDistance))
        {
            return hit.distance;
        }

        return maxFocusPointDistance;
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // (1) Makes right mouse button for rotation
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float verticalRotation = Input.GetAxis("Mouse Y")  * rotationSpeed * Time.deltaTime;

            pitch = Mathf.Clamp(pitch - verticalRotation , minPitch , maxPitch); // Limit the outcome to min and max reset

            transform.RotateAround(focusPoint.position, Vector3.up, horizontalRotation);
            transform.RotateAround(focusPoint.position, transform.right , pitch - transform.eulerAngles.x);

            transform.LookAt(focusPoint);
        }
    }

    private void HandleMovement()
    {
        Vector3 targetPosition = transform.position;

        float vInput = Input.GetAxisRaw("Vertical");
        float hInput = Input.GetAxisRaw("Horizontal");

        //Makes it move not just up or down but forward 
        Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward , Vector3.up).normalized;

        if (vInput > 0)
        {
            targetPosition += flatForward * movementSpeed * Time.deltaTime;
        }

        if (vInput < 0)
        {
            targetPosition -= flatForward * movementSpeed * Time.deltaTime;
        }

        //Makes it move right and left only
        if (hInput > 0)
        {
            targetPosition += transform.right * movementSpeed * Time.deltaTime;
        }

        if (hInput < 0)
        {
            targetPosition -= transform.right * movementSpeed * Time.deltaTime;
        }

        if (Vector3.Distance(levelCenterPoint, targetPosition) > maxDistanceFromCenter)
        {
            targetPosition = levelCenterPoint + (targetPosition - levelCenterPoint).normalized * maxDistanceFromCenter;
        }

        //Makes the camera movement smoother
        transform.position = Vector3.SmoothDamp(transform.position , targetPosition , ref movementVelocity , smoothTime);

    }

    private void HandleMouseMovement()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 positionDifference = Input.mousePosition - lastMousePosition;
            Vector3 moveRight = transform.right * (-positionDifference.x) * mouseMovementSpeed * Time.deltaTime;
            Vector3 moveForward = transform.forward * (-positionDifference.y) * mouseMovementSpeed * Time.deltaTime;

            moveRight.y = 0;
            moveForward.y = 0;

            Vector3 movement = moveRight + moveForward;
            Vector3 targetPosition = transform.position + movement;

            transform.position = Vector3.SmoothDamp(transform.position , targetPosition , ref mouseMovementVelocity , smoothTime);
            lastMousePosition = Input.mousePosition;
        }
    }

    private void HandleEdgeMovement()
    {
        Vector3 targetPosition = transform.position;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);

        if (mousePosition.x > screenWidth - edgeTresHold)
        {
            targetPosition += transform.right * edgeMovementSpeed * Time.deltaTime;
        }

        if (mousePosition.x < edgeTresHold)
        {
            targetPosition -= transform.right * edgeMovementSpeed * Time.deltaTime;
        }

        if (mousePosition.y > screenHeight - edgeTresHold)
        {
            targetPosition += flatForward * edgeMovementSpeed * Time.deltaTime;
        }    

        if (mousePosition.y < edgeTresHold)
        {
            targetPosition -= flatForward * edgeMovementSpeed * Time.deltaTime;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref edgeMovementVelocity, smoothTime);
    }

}
