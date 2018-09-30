using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Player
{
    public class PlayerSwitchWeapon : PlayerComponent
    {
        #region MEMBERS
        private int currentIndexWeapon = 0;
        #endregion

        #region PRIVATE METHODS
        private void OnEnable()
        {
            currentIndexWeapon = player.stats.availableWeapons.IndexOf(player.stats.currentWeapon);
        }

        private void Update()
        {
            if (player.input.weaponScroll != 0)
            {
                SwitchWeapon(player.input.weaponScroll);
            }
            else if(player.input.weaponKeyPressed != -1)
            {
                SetWeapon(player.input.weaponKeyPressed);
            }
        }

        private void SwitchWeapon(float input)
        {
            List<Weapon> weapons = player.stats.availableWeapons;

            int sign =(int)Mathf.Sign(input);
            int currentIndex = weapons.IndexOf(player.stats.currentWeapon);
            int newIndex = currentIndex + sign;
            newIndex = (newIndex < 0) ? newIndex + weapons.Count : newIndex % weapons.Count;
            SetWeapon(newIndex);
        }

        private void SetWeapon(int index)
        {
            if (currentIndexWeapon == index)
            {
                return;
            }
            List<Weapon> weapons = player.stats.availableWeapons;
            Weapon newWeapon = weapons[index];
            player.stats.currentWeapon = newWeapon;
            currentIndexWeapon = index;
        }

        #endregion
    }
}