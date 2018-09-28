using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Utils;

namespace VLTest.Player.Cameras {

    public class PlayerCamera : MonoBehaviour {

        public const float SENSIBILITY_FACTOR = 50;

        public PlayerCameraConfig config;
        public PlayerInput input;
        public Transform pivot;

        private float horizontal
        {
            get { return input.horizontal; }
        }

        private float vertical
        {
            get { return input.vertical; }
        }

        private void Awake()
        {
            if (pivot == null)
            {
                pivot = this.transform;
            }
        }

        private void Update() {

            if (horizontal != 0 || vertical != 0)
            {
                
                // Rotation around Y-axis
                float degrees = horizontal * config.horizontalSensibility * SENSIBILITY_FACTOR;

                Vector3 angles = new Vector3(0, degrees, 0);
                Quaternion cameraRotation = Quaternion.Euler(transform.rotation.eulerAngles + (angles * Time.deltaTime));
                Vector3 position = cameraRotation * (Vector3.forward * -config.distanceToPivot) + pivot.position;
                transform.rotation = cameraRotation;
                transform.position = position;

                // Rotation around X-axis with limits
                degrees = vertical * config.verticalSensibility * SENSIBILITY_FACTOR;

                angles = new Vector3(-degrees, 0, 0);
                cameraRotation = Quaternion.Euler(transform.rotation.eulerAngles + (angles * Time.deltaTime));
                bool limitReached = XAxisLimitsReached(cameraRotation);
                if (!limitReached)
                {
                    position = cameraRotation * (Vector3.forward * -config.distanceToPivot) + pivot.position;
                    transform.rotation = cameraRotation;
                    transform.position = position;
                }
            }

        }

        private bool XAxisLimitsReached(Quaternion xRotation)
        {
            const float FULL_LOOP = 360f;
            const float HALF_LOOP = 180f;

            float xAngle = xRotation.eulerAngles.x;
            xAngle = xAngle >= HALF_LOOP ? xAngle - FULL_LOOP : xAngle;
            return (xAngle > config.maxY || xAngle < config.minY) ? true : false;
        }

    }
}