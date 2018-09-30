using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Player
{
    /// <summary>
    /// Controls each player's input during a game
    /// </summary>
    public class PlayerInput : MonoBehaviour
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
        private string weapon1Input = "Weapon 1";
        [SerializeField]
        private string weapon2Input = "Weapon 2";
        [SerializeField]
        private string weapon3Input = "Weapon 3";

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
        public bool weapon1;
        [HideInInspector]
        public bool weapon2;
        [HideInInspector]
        public bool weapon3;
        #endregion

        #region PRIVATE METHODS
        private void Update()
        {
            horizontal = Input.GetAxis(horizontalAxisInput);
            vertical = Input.GetAxis(verticalAxisInput);
            weaponScroll = Input.GetAxis(weaponScrollInput);
            fire = Input.GetAxis(fireInput) != 0;
            cameraSwitch = Input.GetAxis(cameraSwitchInput) != 0;
            weapon1 = Input.GetAxis(weapon1Input) != 0;
            weapon2 = Input.GetAxis(weapon2Input) != 0;
            weapon3 = Input.GetAxis(weapon3Input) != 0;
        }
        #endregion
    }
}