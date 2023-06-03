using UnityEngine;

namespace UnityCoreHaptics.Demo {
    // This script continuously animates a cube. Its purpose is to show any framerate issues
    // or lags that can be caused by playing haptics.
    // This plugin is designed to maximize performance.
    public class AnimatingCube : MonoBehaviour
    {   
        [SerializeField] float animationSpeed = 2f;

        [SerializeField] float maxRotationDegrees = 60f;

        [SerializeField] Color color1;
        [SerializeField] Color color2;

        // Local variables
        Renderer _renderer;
        Vector3 _startPosition;

        void Start() {
            _renderer = GetComponent<Renderer>();
            _startPosition = transform.position;
        }

        void Update() {
            float sinValue = Mathf.Sin(Time.time * animationSpeed);

            // Rotate cube
            transform.rotation = Quaternion.Euler(0, sinValue * maxRotationDegrees, 0);

            // Move back and forth
            float offsetX = sinValue;
            float offsetY = 0.2f * sinValue * sinValue;
            transform.position = _startPosition + new Vector3(offsetX, offsetY, 0);

            // Interpolate color
            float sin01 = (sinValue + 1) / 2; // Normalized to be between 0 and 1
            Color c = Color.Lerp(color1, color2, sin01);
            _renderer.material.SetColor("_Color", c);
        }
    }
}
