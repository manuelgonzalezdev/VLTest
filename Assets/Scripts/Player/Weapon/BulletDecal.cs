using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Utils;

namespace VLTest.Player.Weapons
{
    public class BulletDecal : ObjectPoolItem
    {

        #region MEMBERS
        public float lifeTime = 0.1f;
        public MeshRenderer render;

        private bool running;
        private float timeRemaining;
        #endregion

        #region PRIVATE METHODS
        public override void Activate()
        {
            base.Activate();
            render.enabled = true;
            running = true;
            timeRemaining = lifeTime;
        }

        public override void Deactivate()
        {
            render.enabled = false;
            running = false;
            base.Deactivate();
        }

        private void LateUpdate()
        {
            if (running)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining <= 0)
                {
                    Deactivate();
                }
            }
        }
        #endregion

    }
}