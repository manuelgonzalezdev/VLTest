using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Enemy
{
    public class EnemyLogic : MonoBehaviour
    {
        public Movements.EnemyMovement movement;

        private void Start()
        {
            Move();
        }

        void Move()
        {
            movement.Move(Move);
        }

    }
}