using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VLTest.Player;
using VLTest.Player.Weapons;

namespace VLTest.UI
{
    public class UIPlayer : MonoBehaviour
    {
        #region MEMBERS
        public PlayerStats stats;
        public Text currentWeaponLabel;
        public Image healthBar;
        #endregion

        #region PRIVATE METHODS
        private void OnEnable()
        {
            stats.OnHealthChanged += OnPlayerHealthChanged;
            stats.OnWeaponChanged += OnPlayerWeaponChanged;

            SetHealth(stats.currentHealth);
            SetWeapon(stats.currentWeapon);
        }

        private void OnDisable()
        {
            stats.OnHealthChanged -= OnPlayerHealthChanged;
            stats.OnWeaponChanged -= OnPlayerWeaponChanged;
        }

        private void SetHealth(float health)
        {
            float percentage = health / stats.maxHealth;
            healthBar.fillAmount = percentage;
        }

        private void SetWeapon(Weapon weapon)
        {
            currentWeaponLabel.text = weapon.weaponName;
        }

        private void OnPlayerHealthChanged(float health)
        {
            SetHealth(health);
        }

        private void OnPlayerWeaponChanged(Weapon weapon)
        {
            SetWeapon(weapon);
        }
        #endregion
    }
}