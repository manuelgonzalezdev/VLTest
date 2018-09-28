using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Enemies.Movement;
using VLTest.Utils;

namespace VLTest.Enemies
{
    /// <summary>
    /// Main class for enemy entities.
    /// It's used to handle load configs when an enemy is spawned from Enemy Pool
    /// and to disable enemy movement when this is deactivated.
    /// </summary>
    public class Enemy : ObjectPoolItem
    {
        public EnemyConfig config;
        public EnemyMovement movement;
        public EnemyDeadEffect deadEffect;

        [SerializeField]
        private float currentHealth;

        public void LoadConfig(EnemyConfig config)
        {
            this.config = config;
            gameObject.name = config.name;
            currentHealth = config.health;
            transform.localScale = Vector3.one * config.size;
            movement.LoadMovements(config);
        }

        public void SetDamage(float damage)
        {
            if (currentHealth > 0)
            {
                currentHealth = Mathf.Max(0, currentHealth - damage);
                if (currentHealth == 0)
                {
                    movement.Stop();
                    deadEffect.PlayDeadEffect(Deactivate);
                }
            }
        }

        public void MoveTowards()
        {
            movement.Play();
        }

        public override void Deactivate()
        {
            movement.Stop();
            base.Deactivate();
        }

        private void Awake()
        {
            if (movement == null)
            {
                movement = GetComponent<EnemyMovement>();
            }
            if (config != null)
            {
                LoadConfig(config);
            }
        }


    }
}