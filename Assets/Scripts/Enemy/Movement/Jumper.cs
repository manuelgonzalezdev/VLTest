using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace VLTest.Enemies.Movement
{
    public class Jumper : MonoBehaviour
    {

        #region MEMBERS
        private bool jumping;
        private float progress;
        private float height;
        private float speed;
        private Vector3 origin;
        private Vector3 destination;
        private Action callback;
        #endregion

        #region PUBLIC METHODS
        public void Jump(float distance, float height, float speed, Action callback)
        {
            if (jumping)
            {
                return;
            }

            jumping = true;
            progress = 0;
            this.height = height;
            this.speed = speed;
            origin = transform.position;
            destination = transform.position + transform.forward * distance;
            this.callback = callback;
        }

        public void Cancel()
        {
            if (jumping)
            {
                transform.position = destination;
                jumping = false;
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
            if (jumping)
            {
                if (progress < 1)
                {
                    progress += speed * Time.deltaTime;
                    float currentHeight = Mathf.Sin(Mathf.PI * progress) * height;
                    transform.position = Vector3.Lerp(origin, destination, progress);
                    transform.position += transform.up * currentHeight;
                }
                else
                {
                    transform.position = destination;
                    jumping = false;
                    if (callback != null)
                    {
                        callback.Invoke();
                    }
                }
            }
        }
        #endregion

    }
}