using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Enemies.Movement;
using VLTest.Utils;

namespace VLTest.Enemies
{
    public class Enemy : ObjectPoolItem
    {
        [System.NonSerialized]
        public EnemyConfig config;
        public EnemyMovement movement;

        public void LoadConfig(EnemyConfig config)
        {
            gameObject.name = config.name;
            this.config = config;
            transform.localScale = Vector3.one * config.size;
            movement.LoadMovements(config);
            movement.Play();
        }

        private void Awake()
        {
            if (movement == null)
            {
                movement = GetComponent<EnemyMovement>();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Deactivate();
            }
        }

        public override void Deactivate()
        {
            movement.Stop();
            base.Deactivate();
        }

    }
}