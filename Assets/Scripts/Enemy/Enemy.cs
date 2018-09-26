using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Enemies.Movement;

namespace VLTest.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [System.NonSerialized]
        public EnemyConfig config;
        public EnemyMovement movement;

        public void LoadConfig(EnemyConfig config)
        {
            this.config = config;
            movement.LoadMovements(config);
        }

        private void Awake()
        {
            if (movement == null)
            {
                movement = GetComponent<EnemyMovement>();
            }
        }

        private void Start()
        {
            movement.enabled = true;
        }

    }
}