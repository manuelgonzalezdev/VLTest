using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace VLTest.Enemy
{
    public class Rotator : MonoBehaviour
    {
        private bool rotating;

        private Quaternion lastRotation;
        private Vector3 pivot;
        private float remainingDegrees;
        private float speed;
        private Action callback;

        public void Rotate(Vector3 pivot, float angle = 90f, float speed = 100f, Action callback = null)
        {
            lastRotation = transform.rotation;
            this.pivot = pivot;
            remainingDegrees = angle;
            this.speed = speed;
            this.callback = callback;

            rotating = true;
        }

        void Update()
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
                    transform.RotateAround(pivot, transform.right, angle);
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
    }
}