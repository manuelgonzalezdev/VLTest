using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Player
{
    [CreateAssetMenu(menuName ="Player/Weapon")]
    public class Weapon : ScriptableObject
    {
        #region MEMBERS
        public string weaponName;
        public float firingRate = 1f;
        public float damage = 10f;
        public float dispersion = 0.1f;
        public int bulletsPerShot = 1;
        #endregion
    }
}