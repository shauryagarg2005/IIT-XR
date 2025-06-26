using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

//Note: This is currently in Development

public class PlayerInputCotroller : MonoBehaviour
{
    public Transform sphereTransform;

    private void Awake()
    {
        if (!EnhancedTouchSupport.enabled)
            EnhancedTouchSupport.Enable();
    }
    public void Pinch(InputAction.CallbackContext context)
    {
        // if there are not two active touches, return
        if (Touch.activeTouches.Count < 2)
            return;
        // get the finger inputs
        Touch primary = Touch.activeTouches[0];
        Touch secondary = Touch.activeTouches[1];
        // check if none of the fingers moved, return
        if (primary.phase == TouchPhase.Moved || secondary.phase == TouchPhase.Moved)
        {
            // if fingers have no history, then return
            if (primary.history.Count < 1 || secondary.history.Count < 1)
                return;
            // calculate distonce before and after touch movement
            float currentDistance = Vector2.Distance(a:primary.screenPosition, b:secondary.screenPosition);
            float previousDistance = Vector2.Distance(a:primary.history[0].screenPosition, b:secondary.history[0].screenPosition);
            // the zoom distance is the difference between the previous distance and the current distance
            float pinchDistance = currentDistance - previousDistance;
            Zoom(pinchDistance);
        }
    }

    public void Scroll(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
            return;
        float scrollDistance = context.ReadValue<Vector2>().y;
        Zoom(scrollDistance);
    }
    public void Zoom(float distance)
    {
        distance = distance * 0.01f;
        sphereTransform.localScale += new Vector3( x: distance, y: distance,z: distance);
    }
}