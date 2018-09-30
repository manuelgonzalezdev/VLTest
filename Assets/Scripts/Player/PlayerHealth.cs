using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Player
{
    public class PlayerHealth : PlayerComponent
    {
        #region MEMBERS
        public float health
        {
            get { return player.stats.currentHealth; }
            set
            {
                player.stats.currentHealth = value;
                if (player.stats.currentHealth <= 0)
                {
                    player.Kill();   
                }
            }
        }
        #endregion

        #region PUBLIC METHODS

        public void SetDamage(float damage)
        {
            if (health > 0)
            {
                health = Mathf.Max(0, health - damage);
            }
        }

        #endregion

        #region PRIVATE METHODS
        private void OnEnable()
        {
            health = player.stats.maxHealth;
        }
        #endregion
    }
}