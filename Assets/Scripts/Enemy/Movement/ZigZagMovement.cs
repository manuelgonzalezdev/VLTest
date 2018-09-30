using UnityEngine;
using System;
using VLTest.Utils;

namespace VLTest.Enemies.Movement
{
    [CreateAssetMenu(menuName = "Enemy/Movements/Zig-zag")]
    public class ZigZagMovement : EnemyMovementType
    {
        #region PUBLIC METHODS
        public override EnemyMovementLogic CreateMovement(GameObject enemy)
        {
            return new ZigZagLogic(enemy);
        }
        #endregion
    }

    public class ZigZagLogic : EnemyMovementLogic
    {
        #region MEMBERS
        private const float ANGLE = 90f;

        private Rotator rotator;
        private bool right;
        #endregion

        #region PUBLIC METHODS
        public ZigZagLogic(GameObject enemy) : base(enemy)
        {
            rotator = Utils.Utils.GetComponentOrCreateIfNotExists<Rotator>(enemy);
        }

        public override bool Move(float speed, Action callback, ObjectPool projectionPool)
        {
            float size = transform.localScale.z;
            Vector3 direction = (right ? Vector3.right : -Vector3.right);
            Vector3 localDirection = transform.TransformDirection(direction * size);
            Vector3 destination = transform.position + localDirection;
            
            if (MovementIsValid(destination, size, transform.rotation))
            {
                this.callback = callback;

                Vector3 pivot = transform.position + (transform.right * (right ? 0.5f : -0.5f) * transform.localScale.x);
                pivot.y -= transform.localScale.y * 0.5f;
                rotator.Rotate(pivot, right ? -transform.forward : transform.forward, ANGLE, speed, OnMovementFinished);

                this.projection = CreateProjection(projectionPool, right ? Vector3.right : -Vector3.right);

                right = !right;
                return true;
            }

            return false;
        }

        public override void Cancel()
        {
            base.Cancel();
            rotator.Cancel();
        }
        #endregion
    }
}