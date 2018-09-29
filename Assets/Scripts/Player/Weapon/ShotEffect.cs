using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Player
{

    public class ShotEffect : MonoBehaviour
    {

        public float duration = 0.2f;
        public GameObject render;

        private bool running;
        private float timeRemaining;

        public void PlayEffect()
        {
            if (running)
            {
                return;
            }
            render.SetActive(true);
            running = true;
            timeRemaining = duration;
        }

        private void Awake()
        {
            render.SetActive(false);
        }

        private void LateUpdate()
        {
            if (running)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining <= 0)
                {
                    render.SetActive(false);
                    running = false;
                }
            }
        }


    }
}