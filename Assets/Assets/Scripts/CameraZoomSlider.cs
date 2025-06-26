using UnityEngine;
using UnityEngine.UI;

namespace Shaurya_Sample
{
    public class CameraZoomSlider : MonoBehaviour
    {
        public Camera targetCamera;
        public Slider zoomSlider;
        public float zoomAmount = 2f;
        private float defaultZ;

        private void Start()
        {
            defaultZ = targetCamera.transform.position.z;
            OnZoomValueChanged(zoomSlider.value);
            zoomSlider.onValueChanged.AddListener(OnZoomValueChanged);
        }

        public void OnZoomValueChanged(float value)
        {
            float offset = Mathf.Lerp(-zoomAmount, zoomAmount, value);

            Vector3 pos = targetCamera.transform.position;
            pos.z = defaultZ + offset;
            targetCamera.transform.position = pos;
        }
    }
}