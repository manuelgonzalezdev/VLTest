using UnityEngine;
using System;
using VLTest.Utils;

namespace VLTest.Enemy.Movement
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

        public ForwardMovementLogic(GameObject enemy)
        {
            transform = enemy.transform;
            rotator = GameObjectUtils.GetComponentOrCreateIfNotExists<Rotator>(enemy);
        }

        public override void Move(float speed, Action callback)
        {
            Vector3 pivot = transform.position + (transform.forward * 0.5f);
            pivot.y -= 0.5f;
            rotator.Rotate(pivot, transform.right, ANGLE, speed, callback);
        }
    }

}