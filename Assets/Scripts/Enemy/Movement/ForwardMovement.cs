using UnityEngine;
using System;
using VLTest.Utils;

namespace VLTest.Enemies.Movement
{
    [CreateAssetMenu(menuName = "Enemy/Movements/Forward")]
    public class ForwardMovement : EnemyMovementType
    {
        public override EnemyMovementLogic CreateMovement(GameObject enemy)
        {
            return new ForwardMovementLogic(enemy);
        }
    }

    public class ForwardMovementLogic : EnemyMovementLogic
    {
        private const float ANGLE = 90f;

        private Rotator rotator;

        public ForwardMovementLogic(GameObject enemy) : base(enemy)
        {
            rotator = Utils.Utils.GetComponentOrCreateIfNotExists<Rotator>(enemy);
        }

        public override bool Move(float speed, Action callback, ObjectPool projectionPool)
        {
            float size = transform.localScale.z;
            Vector3 direction = Vector3.forward;
            Vector3 localDirection = transform.TransformDirection(direction * size);
            Vector3 destination = transform.position + localDirection;
            if (MovementIsValid(destination, size, transform.rotation))
            {
                this.callback = callback;

                Vector3 pivot = transform.position + (transform.forward * 0.5f * transform.localScale.z);
                pivot.y -= transform.localScale.y * 0.5f;
                rotator.Rotate(pivot, transform.right, ANGLE, speed, OnMovementFinished);

                this.projection = CreateProjection(projectionPool, direction);
                return true;
            }

            return false;
        }

        public override void Cancel()
        {
            base.Cancel();
            rotator.Cancel();
        }
    }

}