using UnityEngine;
using System;
using VLTest.Utils;

namespace VLTest.Enemies.Movement
{
    [CreateAssetMenu(menuName = "Enemy/Movements/Zig-zag")]
    public class ZigZagMovement : EnemyMovementType
    {

        public override EnemyMovementLogic CreateMovement(GameObject enemy)
        {
            return new ZigZagLogic(enemy);
        }

    }

    public class ZigZagLogic : EnemyMovementLogic
    {
        private const float ANGLE = 90f;

        private Rotator rotator;
        private bool right;

        public ZigZagLogic(GameObject enemy)
        {
            transform = enemy.transform;
            rotator = GameObjectUtils.GetComponentOrCreateIfNotExists<Rotator>(enemy);
        }

        public override void Move(float speed, Action callback)
        {
            Vector3 pivot = transform.position + (transform.right * (right ? 0.5f : -0.5f));
            pivot.y -= 0.5f;
            rotator.Rotate(pivot, right ? -transform.forward : transform.forward, ANGLE, speed, callback);
            right = !right;
        }

        public override void Cancel()
        {
            rotator.Cancel();
        }

    }
}