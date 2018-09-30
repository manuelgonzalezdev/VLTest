using UnityEngine;
using System;

namespace VLTest.Enemies
{
    public class EnemyDeadEffect : EnemyComponent
    {
        #region MEMBERS
        private const float INITIAL_ALPHA = 1f;
        private const float EFFECT_DURATION = 0.5f;

        public MeshRenderer enemyRenderer;

        private Action callback;
        private bool running;
        private float alpha;
        #endregion

        #region PUBLIC METHODS
        public void PlayDeadEffect(Action callback = null)
        {
            if (running)
            {
                return;
            }
            this.callback = callback;
            SetAlpha(1f);
            enemy.collider.enabled = false;
            running = true;
        }
        #endregion

        #region PRIVATE METHODS
        private void OnEnable()
        {
            SetAlpha(INITIAL_ALPHA);
        }

        private void Update()
        {
            if (running)
            {
                float newAlpha = Mathf.Max(0f, alpha - Time.deltaTime);
                SetAlpha(newAlpha);
                if (alpha == 0f)
                {
                    running = false;
                    if (callback != null)
                    {
                        callback.Invoke();
                    }
                }
            }
        }

        private void SetAlpha(float newAlpha)
        {
            if (alpha == newAlpha)
            {
                return;
            }
            Color c = enemyRenderer.material.color;
            c.a = newAlpha;
            enemyRenderer.material.color = c;
            alpha = newAlpha;
        }
        #endregion
    }
}