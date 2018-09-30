using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Game
{
    [CreateAssetMenu(menuName = "Game/Scenes")]
    public class Scenes : ScriptableObject
    {
        #region MEMBERS
        public string menuScene = "Menu";
        public string gameScene = "Game";
        #endregion
    }
}