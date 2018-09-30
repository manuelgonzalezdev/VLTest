using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Commons
{
    [CreateAssetMenu(menuName = "Commons/Scenes")]
    public class Scenes : ScriptableObject
    {
        #region MEMBERS
        public string menuScene = "Menu";
        public string gameScene = "Game";
        #endregion
    }
}