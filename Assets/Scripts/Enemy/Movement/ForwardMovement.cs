using UnityEngine;
using System;

namespace VLTest.Enemy.Movements
{
    [RequireComponent(typeof(Rotator))]
    public class ForwardMovement : EnemyMovement
    {
        private const float ANGLE = 90f;
        private const float SPEED = 100f;

        private Rotator rotator;

        private void Awake()
        {
            rotator = GetComponent<Rotator>();
        }

        public override void Move(Action callback)
        {
            Vector3 pivot = transform.position + (transform.forward * 0.5f);
            pivot.y -= 0.5f;
            rotator.Rotate(pivot, transform.right, ANGLE, SPEED, callback);
        }

    }
}