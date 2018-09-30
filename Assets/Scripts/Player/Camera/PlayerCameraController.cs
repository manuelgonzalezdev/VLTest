using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Player.Cameras {

    /// <summary>
    /// Manages active camera and the camera switching
    /// </summary>
    public class PlayerCameraController : MonoBehaviour {

        #region EVENTS
        public delegate void OnCameraChangedEvent(PlayerCamera newCamera);
        public static event OnCameraChangedEvent OnCameraChanged;
        #endregion

        #region MEMBERS
        public PlayerInput input;
        public PlayerCamera[] cameras;

        public PlayerCamera currentCamera
        {
            get { return cameras[currentCameraIndex]; }
        }

        // Flag to avoid burst input
        private bool cameraChanged;
        private int currentCameraIndex;
        #endregion

        #region PRIVATE METHODS
        private void Awake()
        {
            ChangeToCamera(0);
        }

        private void Update() {
            if (input.cameraSwitch && !cameraChanged)
            {
                int nextIndex = (currentCameraIndex + 1) % cameras.Length;
                ChangeToCamera(nextIndex);
                cameraChanged = true;
            }
            else if (cameraChanged && !input.cameraSwitch)
            {
                cameraChanged = false;
            }
        }

        private void ChangeToCamera(int cameraIndex)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                PlayerCamera camera = cameras[i];
                camera.gameObject.SetActive(i == cameraIndex);
                bool hasChanged = cameraIndex != currentCameraIndex;
                currentCameraIndex = cameraIndex;
                if (hasChanged && OnCameraChanged != null)
                {
                    OnCameraChanged(currentCamera);
                }
            }
        }
        #endregion
    }
}