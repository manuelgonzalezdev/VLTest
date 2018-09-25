using UnityEngine;
using System;
using VLTest.Utils;

namespace VLTest.Enemy.Movement
{
    [CreateAssetMenu(menuName = "Enemy/Movements/Jumping")]
    public class JumpingMovement : EnemyMovementType
    {

        public float jumpingDistance = 1f;
        public float jumpingHeight = 1f;

        public override EnemyMovementLogic CreateMovement(GameObject enemy)
        {
            return new JumpingLogic(enemy, jumpingDistance, jumpingHeight);
        }
    }

    public class JumpingLogic : EnemyMovementLogic
    {
        private const float DISTANCE = 1f;
        private const float HEIGHT = 1f;

        private Jumper jumper;
        private float distance;
        private float height;

        public JumpingLogic(GameObject enemy, float distance,  float height)
        {
            jumper = GameObjectUtils.GetComponentOrCreateIfNotExists<Jumper>(enemy);
            this.distance = distance;
            this.height = height;
        }

        public override void Move(float speed, Action callback)
        {
            jumper.Jump(DISTANCE, height, speed, callback);


        }
    }
}
