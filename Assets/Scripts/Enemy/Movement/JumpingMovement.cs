using UnityEngine;
using System;
using VLTest.Utils;

namespace VLTest.Enemies.Movement
{
    [CreateAssetMenu(menuName = "Enemy/Movements/Jumping")]
    public class JumpingMovement : EnemyMovementType
    {

        public float jumpingSpeed = 1f;
        public float jumpingDistance = 1f;
        public float jumpingHeight = 1f;

        public override EnemyMovementLogic CreateMovement(GameObject enemy)
        {
            return new JumpingLogic(enemy, jumpingSpeed, jumpingDistance, jumpingHeight);
        }
    }

    public class JumpingLogic : EnemyMovementLogic
    {

        private Jumper jumper;
        private float jumpingSpeed;
        private float jumpingDistance;
        private float jumpingHeight;
        private float size;

        public JumpingLogic(GameObject enemy, float jumpingSpeed, float jumpingDistance,  float jumpingHeight) : base(enemy)
        {
            jumper = Utils.Utils.GetComponentOrCreateIfNotExists<Jumper>(enemy);
            this.jumpingSpeed = jumpingSpeed;
            this.jumpingDistance = jumpingDistance;
            this.jumpingHeight = jumpingHeight;
            this.size = enemy.GetComponent<Enemy>().config.size;
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
                jumper.Jump(jumpingDistance * size, jumpingHeight * size, speed * jumpingSpeed, OnMovementFinished);

                this.projection = CreateProjection(projectionPool, Vector3.forward);
                return true;
            }

            return false;
        }

        public override void Cancel()
        {
            base.Cancel();
            jumper.Cancel();
        }
    }
}
