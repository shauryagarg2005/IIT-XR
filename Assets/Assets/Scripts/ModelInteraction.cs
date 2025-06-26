using UnityEngine;
//Note: this is currently in Development
public class ModelInteraction : MonoBehaviour
{
    [Header("Zoom Settings")]
    public float zoomSpeedTouch = 0.1f;
    public float zoomSpeedMouse = 10f;
    public float minZoom = 0.5f;
    public float maxZoom = 5f;

    [Header("Rotation Settings")]
    public float rotationSpeedTouch = 0.2f;
    public float rotationSpeedMouse = 5f;

    private Vector3 initialScale;
    private Vector3 lastMousePosition;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    private void HandleMouseInput()
    {
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            float scaleFactor = 1 + scroll * zoomSpeedMouse;
            Vector3 newScale = transform.localScale * scaleFactor;
            transform.localScale = ClampScale(newScale);
        }

        
        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotX = -delta.y * rotationSpeedMouse * Time.deltaTime;
            float rotY = delta.x * rotationSpeedMouse * Time.deltaTime;
            transform.Rotate(Vector3.up, rotY, Space.World);
            transform.Rotate(Vector3.right, rotX, Space.World);
        }

        lastMousePosition = Input.mousePosition;
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float rotX = -touch.deltaPosition.y * rotationSpeedTouch;
                float rotY = touch.deltaPosition.x * rotationSpeedTouch;
                transform.Rotate(Vector3.up, rotY, Space.World);
                transform.Rotate(Vector3.right, rotX, Space.World);
            }
        }
        else if (Input.touchCount == 2)
        {
            
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            float prevDist = (t0.position - t0.deltaPosition - (t1.position - t1.deltaPosition)).magnitude;
            float currDist = (t0.position - t1.position).magnitude;

            float delta = currDist - prevDist;
            float scaleFactor = 1 + delta * zoomSpeedTouch * Time.deltaTime;
            Vector3 newScale = transform.localScale * scaleFactor;
            transform.localScale = ClampScale(newScale);
        }
    }

    private Vector3 ClampScale(Vector3 scale)
    {
        float clampedX = Mathf.Clamp(scale.x, initialScale.x * minZoom, initialScale.x * maxZoom);
        float clampedY = Mathf.Clamp(scale.y, initialScale.y * minZoom, initialScale.y * maxZoom);
        float clampedZ = Mathf.Clamp(scale.z, initialScale.z * minZoom, initialScale.z * maxZoom);
        return new Vector3(clampedX, clampedY, clampedZ);
    }
}
