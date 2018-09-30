using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Commons
{
    [CreateAssetMenu(menuName = "Commons/Inputs")]
    public class Inputs : ScriptableObject
    {
        #region MEMBERS
        public string horizontalAxisInput = "Mouse X";
        public string verticalAxisInput = "Mouse Y";
        public string fireInput = "Fire";
        public string cameraSwitchInput = "Camera Switch";
        public string weaponScrollInput = "Mouse ScrollWheel";
        public string weaponPrefix = "Weapon {0}";
        public string pauseInput = "Pause";
        #endregion
    }
}