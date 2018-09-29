using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Player
{
    public class PlayerSwitchWeapon : PlayerComponent
    {

        private void Update()
        {
            if (player.input.weaponScroll != 0)
            {
                SwitchWeapon(player.input.weaponScroll);
            }
        }

        private void SwitchWeapon(float input)
        {
            List<Weapon> weapons = player.stats.availableWeapons;

            int sign =(int)Mathf.Sign(input);
            int currentIndex = weapons.IndexOf(player.stats.currentWeapon);
            int newIndex = currentIndex + sign;
            newIndex = (newIndex < 0) ? newIndex + weapons.Count : newIndex % weapons.Count;
            Weapon newWeapon = weapons[newIndex];
            player.stats.currentWeapon = newWeapon;
        }


    }
}