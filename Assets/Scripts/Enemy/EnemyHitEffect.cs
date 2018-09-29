using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Enemies {

    public class EnemyHitEffect : MonoBehaviour {

        private const float EFFECT_DURATION = 0.1f;

        public MeshRenderer enemyRenderer;
        public Color hitColor;

        private bool running;
        private Color initialColor;
        private float timeRemaining;

        public void PlayHitEffect()
        {
            if (running)
            {
                return;
            }
            SetColor(hitColor);
            timeRemaining = EFFECT_DURATION;
            running = true;
        }

        private void Awake()
        {
            initialColor = enemyRenderer.material.color;
        }

        private void OnEnable()
        {
            SetColor(initialColor);
            running = false;
        }

        private void Update()
        {
            if (running)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining <= 0)
            {
                    SetColor(initialColor);
                    running = false;
                }
            }
        }

        private void SetColor(Color color)
        {
            enemyRenderer.material.color = color;
        }

    }
}