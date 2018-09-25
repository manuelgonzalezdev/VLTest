using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Enemy.Movement;

namespace VLTest.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public EnemyMovement movement;

        public EnemyConfig config;

        private void Awake()
        {
            movement = GetComponent<EnemyMovement>();
            movement.LoadMovements(config);
            movement.enabled = true;
        }

    }
}