using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Player.Cameras {

    public class RearPlayerCamera : PlayerComponent {

        #region PRIVATE METHODS
        private void Update()
        {
            transform.forward = -player.cameraController.currentCamera.transform.forward;
        }

        private Vector3 GetForwardDirection(PlayerCamera camera)
        {
            Vector3 forward = (camera.transform.position - camera.pivot.position).normalized;
            forward.y = 0;
            return forward;
        }
        #endregion

    }
}