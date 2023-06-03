using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCoreHaptics.Demo {
    public class Demo : MonoBehaviour
    {
        [SerializeField] Text DeviceSupported;

        [SerializeField] Button PlayTransient;
        [SerializeField] Button PlayContinuous;
        [SerializeField] Button PlayCustomAHAP;

        [Tooltip("Relative to Assets/StreamingAssets")]
        [SerializeField] string ahapRelativePath;

        void Start() {
            // Set to portrait mode
            Screen.orientation = ScreenOrientation.Portrait;

            // Indicate whether the device supports Core Haptics or not
            DeviceSupported.text = "Device supported: " + UnityCoreHapticsProxy.SupportsCoreHaptics();

            // Check if relative path to AHAP is correct
            // Note: this demo won't play AHAP files in its current state
            // You must import an AHAP into Assets/StreamingAssets and ahapRelativePath to it
            // Please see README.pdf for more details
            if (ahapRelativePath != null && ahapRelativePath != "") {
                Utility.AssertFileExists(ahapRelativePath);
            }

            // Create haptic engine before using it to play haptics
            UnityCoreHapticsProxy.CreateEngine();

            // Set up callbacks
            UnityCoreHapticsProxy.OnEngineCreated += () => {
                Debug.Log("Unity: ENGINE CREATED");
            };
            
            UnityCoreHapticsProxy.OnEngineError += () => {
                Debug.LogError("Unity: ENGINE ERROR");
            };

            // Listen to click events
            PlayTransient.onClick.AddListener(PlayTransientHaptics);
            PlayContinuous.onClick.AddListener(PlayContinuousHaptics);
            PlayCustomAHAP.onClick.AddListener(PlayHapticsFromAHAP);
        }

        void PlayTransientHaptics() {
            Debug.Log("Playing transient!");
            UnityCoreHapticsProxy.PlayTransientHaptics(1f, 1f);
        }

        void PlayContinuousHaptics() {
            Debug.Log("Playing continuous!");
            UnityCoreHapticsProxy.PlayContinuousHaptics(1f, 0.2f, 1f);
        }

        void PlayHapticsFromAHAP() {
            Debug.Log("Playing AHAP");
            UnityCoreHapticsProxy.PlayHapticsFromFile(ahapRelativePath);
        }
    }
}