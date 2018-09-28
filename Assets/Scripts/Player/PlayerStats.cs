using UnityEngine;

namespace VLTest.Player
{
    [CreateAssetMenu(menuName = "Player/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        public delegate void OnHealthChangedEvent(float newHealth);
        public delegate void OnWeaponChangedEvent(Weapon newWeapon);
        public event OnHealthChangedEvent OnHealthChanged;
        public event OnWeaponChangedEvent OnWeaponChanged;

        public float maxHealth = 100;
        public Weapon initialWeapon;
        public Weapon[] availableWeapons;

        private float _currentHealth;

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

        private Weapon _currentWeapon;

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

        [System.NonSerialized] private bool initialized = false;

        private void Initialize()
        {
            _currentHealth = maxHealth;
            _currentWeapon = initialWeapon;
            initialized = true;
        }

    }
}