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

        public JumpingLogic(GameObject enemy, float jumpingSpeed, float jumpingDistance,  float jumpingHeight)
        {
            jumper = GameObjectUtils.GetComponentOrCreateIfNotExists<Jumper>(enemy);
            this.jumpingSpeed = jumpingSpeed;
            this.jumpingDistance = jumpingDistance;
            this.jumpingHeight = jumpingHeight;
        }

        public override void Move(float speed, Action callback)
        {
            jumper.Jump(jumpingDistance, jumpingHeight, speed * jumpingSpeed, callback);
        }

        public override void Cancel()
        {
            jumper.Cancel();
        }
    }
}
