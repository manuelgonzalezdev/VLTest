using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VLTest.Player;

namespace VLTest.UI
{
    public class UIPlayerPanel : MonoBehaviour
    {
        public PlayerStats stats;
        public Text currentWeaponLabel;
        public Image healthBar;

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
            float percentage = health * stats.maxHealth;
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

    }
}