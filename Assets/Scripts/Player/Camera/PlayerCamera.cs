using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Utils;

namespace VLTest.Player.Cameras
{
    public class PlayerCamera : PlayerComponent
    {
        #region MEMBERS
        /// <summary>
        /// Factor to apply a sensibility base to each axes.
        /// </summary>
        public const float SENSIBILITY_FACTOR = 50;

        /// <summary>
        /// Camera will orbit around this transform
        /// </summary>
        public Transform pivot;

        public PlayerCameraConfig config;

        private PlayerInput input
        {
            get { return player.input; }
        }

        private float horizontal
        {
            get { return input.horizontal; }
        }

        private float vertical
        {
            get { return input.vertical; }
        }
        #endregion

        #region PRIVATE METHODS
        private void Awake()
        {
            if (pivot == null)
            {
                pivot = this.transform;
            }
        }

        private void Update()
        {
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
        #endregion
    }
}