using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Enemies.Movement;
using VLTest.Game;
using VLTest.Utils;

namespace VLTest.Enemies
{
    /// <summary>
    /// Main class for enemy entities.
    /// It's used to handle load configs when an enemy is spawned from Enemy Pool
    /// and to disable enemy movement when this is deactivated or killed.
    /// </summary>
    public class Enemy : ObjectPoolItem
    {
        #region MEMBERS
        public delegate void OnEnemyeadEvent(EnemyConfig config);
        public static event OnEnemyeadEvent OnEnemyKilled;

        public EnemyConfig config;
        public EnemyHealth enemyHealth;
        public EnemyMovement movement;
        public EnemyAttack attack;
        public EnemyDeadEffect deadEffect;
        public EnemyHitEffect hitEffect;
        public new Collider collider;
        #endregion

        #region PUBLIC METHODS
        public void LoadConfig(EnemyConfig config)
        {
            this.config = config;
            gameObject.name = gameObject.name.Replace("SimpleCube", config.name);
            enemyHealth.SetHealth(config.health);
            transform.localScale = Vector3.one * config.size;
            movement.LoadMovements(config);
        }

        public void Kill()
        {
            if (OnEnemyKilled != null)
            {
                OnEnemyKilled(this.config);
            }
            Dead();
        }

        public void Dead()
        {
            movement.Stop();
            deadEffect.PlayDeadEffect(Deactivate);
        }

        public void MoveTowards()
        {
            movement.Play();
        }

        public override void Activate()
        {
            base.Activate();
            collider.enabled = true;
        }

        public override void Deactivate()
        {
            movement.Stop();
            base.Deactivate();
        }

        #endregion

        #region PRIVATE METHODS

        private void Awake()
        {
            collider = GetComponent<Collider>();
            if (config != null)
            {
                LoadConfig(config);
            }
        }

        private void OnEnable()
        {
            GameStateManager.OnGameStateChanges += OnGameStateChanged;
        }

        private void OnDisable()
        {
            GameStateManager.OnGameStateChanges -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameStateManager.GameState last, GameStateManager.GameState newState)
        {
            if (newState == GameStateManager.GameState.WIN || newState == GameStateManager.GameState.LOSE)
            {
                Dead();
            }
            else if (newState == GameStateManager.GameState.MENU)
            {
                Deactivate();
            }
            
        }
        #endregion

    }
}