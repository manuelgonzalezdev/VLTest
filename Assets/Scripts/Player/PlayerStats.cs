using System.Collections.Generic;
using UnityEngine;
using VLTest.Player.Weapons;

namespace VLTest.Player
{
    [CreateAssetMenu(menuName = "Player/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        #region EVENTS
        public delegate void OnHealthChangedEvent(float newHealth);
        public delegate void OnWeaponChangedEvent(Weapon newWeapon);
        public event OnHealthChangedEvent OnHealthChanged;
        public event OnWeaponChangedEvent OnWeaponChanged;
        #endregion

        #region MEMBERS
        public float maxHealth = 100;
        public Weapon initialWeapon;
        public List<Weapon> availableWeapons;

        [System.NonSerialized] private bool initialized = false;

        [System.NonSerialized] private float _currentHealth;
        public float currentHealth
        {
            get
            {
                if (!initialized)
                {
                    Initialize();
                }
                return _currentHealth;
            }
            set
            {
                if (!initialized)
                {
                    Initialize();
                }
                _currentHealth = Mathf.Max(value, 0);
                if (OnHealthChanged != null)
                {
                    OnHealthChanged(_currentHealth);
                }
            }
        }

        [System.NonSerialized] private Weapon _currentWeapon;
        public Weapon currentWeapon
        {
            get
            {
                if (!initialized)
                {
                    Initialize();
                }
                return _currentWeapon;
            }
            set
            {
                if (!initialized)
                {
                    Initialize();
                }
                _currentWeapon = value;
                if (OnWeaponChanged != null)
                {
                    OnWeaponChanged(_currentWeapon);
                }
            }
        }
        #endregion

        #region PRIVATE METHODS
        private void Initialize()
        {
            _currentHealth = maxHealth;
            _currentWeapon = initialWeapon;
            if (!availableWeapons.Contains(initialWeapon))
            {
                availableWeapons.Insert(0, initialWeapon);
            }
            initialized = true;
        }
        #endregion
    }
}