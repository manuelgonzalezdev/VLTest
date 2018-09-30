using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Commons;

namespace VLTest.Player
{
    /// <summary>
    /// Controls each player's input during a game
    /// </summary>
    public class PlayerInput : PlayerComponent
    {
        #region MEMBERS

        public Inputs inputs;

        [HideInInspector]
        public float horizontal;
        [HideInInspector]
        public float vertical;
        [HideInInspector]
        public float weaponScroll;
        [HideInInspector]
        public bool fire;
        [HideInInspector]
        public bool cameraSwitch;
        [HideInInspector]
        public int weaponKeyPressed;
        #endregion

        #region PRIVATE METHODS
        private void Update()
        {
            horizontal = Input.GetAxis(inputs.horizontalAxisInput);
            vertical = Input.GetAxis(inputs.verticalAxisInput);
            weaponScroll = Input.GetAxis(inputs.weaponScrollInput);
            fire = Input.GetAxis(inputs.fireInput) != 0;
            cameraSwitch = Input.GetAxis(inputs.cameraSwitchInput) != 0;

            weaponKeyPressed = -1;
            for (int i = 0; i < player.stats.availableWeapons.Count; i++)
            {
                string axis = string.Format(inputs.weaponPrefix, (i + 1));
                weaponKeyPressed = Input.GetAxis(axis) != 0 ? i : -1;
                if (weaponKeyPressed != -1)
                {
                    break;
                }
            }
        }
        #endregion
    }
}