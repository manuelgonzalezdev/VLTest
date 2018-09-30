using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Commons
{
    [CreateAssetMenu(menuName = "Commons/Layers")]
    public class Layers : ScriptableObject
    {
        #region MEMBERS
        public string enemyLayerName = "Enemy";
        public string playerLayerName = "Player";
        public string projectionLayerName = "Projection";
        public string scenarioLayerName = "Scenario";
        #endregion
    }
}