using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace VLTest.Enemies.Movement
{
    public class Rotator : MonoBehaviour
    {
        #region MEMBERS
        private bool rotating;
        private Quaternion lastRotation;
        private Vector3 pivot;
        private Vector3 axis;
        private float remainingDegrees;
        private float speed;
        private Action callback;
        #endregion

        #region PUBLIC METHODS
        public void Rotate(Vector3 pivot, Vector3 axis, float angle = 90f, float speed = 100f, Action callback = null)
        {
            lastRotation = transform.rotation;
            this.pivot = pivot;
            this.axis = axis;
            remainingDegrees = angle;
            this.speed = speed;
            this.callback = callback;

            rotating = true;
        }

        public void Cancel()
        {
            if (rotating)
            {
                transform.rotation = lastRotation;
                rotating = false;
            }
        }
        #endregion

        #region PRIVATE METHODS
        private void OnDisable()
        {
            Cancel();
        }

        private void Update()
        {
            if (rotating)
            {
                if (remainingDegrees > 0f)
                {
                    float angle = speed * Time.deltaTime;
                    if (remainingDegrees - angle < 0)
                    {
                        angle = remainingDegrees;
                    }
                    transform.RotateAround(pivot, axis, angle);
                    remainingDegrees -= angle;
                }
                else
                {
                    transform.rotation = lastRotation;
                    rotating = false;
                    if (callback != null)
                    {
                        callback.Invoke();
                    }
                }
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(pivot, 0.1f);
        }
#endif
#endregion
    }
}