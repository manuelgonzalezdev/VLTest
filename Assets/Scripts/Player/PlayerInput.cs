using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Player
{
    /// <summary>
    /// Controls each player's input during a game
    /// </summary>
    public class PlayerInput : PlayerComponent
    {
        #region MEMBERS

        [SerializeField]
        private string horizontalAxisInput = "Mouse X";
        [SerializeField]
        private string verticalAxisInput = "Mouse Y";
        [SerializeField]
        private string fireInput = "Fire";
        [SerializeField]
        private string cameraSwitchInput = "Camera Switch";
        [SerializeField]
        private string weaponScrollInput = "Mouse ScrollWheel";
        [SerializeField]
        private string weaponPrefix = "Weapon {0}";

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
            horizontal = Input.GetAxis(horizontalAxisInput);
            vertical = Input.GetAxis(verticalAxisInput);
            weaponScroll = Input.GetAxis(weaponScrollInput);
            fire = Input.GetAxis(fireInput) != 0;
            cameraSwitch = Input.GetAxis(cameraSwitchInput) != 0;

            weaponKeyPressed = -1;
            for (int i = 0; i < player.stats.availableWeapons.Count; i++)
            {
                string axis = string.Format(weaponPrefix, (i + 1));
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