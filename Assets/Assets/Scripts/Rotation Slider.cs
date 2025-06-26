using UnityEngine;
using UnityEngine.UI;

public class RotationSlider : MonoBehaviour
{
    public GameObject target;

    [Header("Sliders")]
    public Slider rotateXSlider;
    public Slider rotateYSlider;
    public Slider rotateZSlider;

    [Header("Rotation Amount (± range)")]
    public float rotateAmount = 180f;

    private Vector3 defaultRotation;

    private void Start()
    {
        // Store the default rotation in Euler angles
        defaultRotation = target.transform.eulerAngles;

        // Initialize rotations
        ApplyRotation();

        // Add listeners for all 3 sliders
        rotateXSlider.onValueChanged.AddListener(delegate { ApplyRotation(); });
        rotateYSlider.onValueChanged.AddListener(delegate { ApplyRotation(); });
        rotateZSlider.onValueChanged.AddListener(delegate { ApplyRotation(); });
    }

    private void ApplyRotation()
    {
        float xOffset = Mathf.Lerp(-rotateAmount, rotateAmount, rotateXSlider.value);
        float yOffset = Mathf.Lerp(-rotateAmount, rotateAmount, rotateYSlider.value);
        float zOffset = Mathf.Lerp(-rotateAmount, rotateAmount, rotateZSlider.value);

        Vector3 newRotation = new Vector3(
            defaultRotation.x + xOffset,
            defaultRotation.y + yOffset,
            defaultRotation.z + zOffset
        );

        target.transform.eulerAngles = newRotation;
    }
}
