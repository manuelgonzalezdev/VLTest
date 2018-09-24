using UnityEngine;
using System;

namespace VLTest.Enemy.Movements
{
    [RequireComponent(typeof(Rotator))]
    public class ZigZagMovement : EnemyMovement
    {
        private const float ANGLE = 90f;
        private const float SPEED = 100f;

        private Rotator rotator;
        private bool right;

        private void Awake()
        {
            rotator = GetComponent<Rotator>();
        }

        public override void Move(Action callback)
        {
            Vector3 pivot = transform.position + (transform.right * (right ? 0.5f : -0.5f));
            pivot.y -= 0.5f;
            rotator.Rotate(pivot, right ? -transform.forward : transform.forward, ANGLE, SPEED, callback);
            right = !right;
        }

    }
}