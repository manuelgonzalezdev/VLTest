using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Player {
    public class PlayerComponent : MonoBehaviour {

        #region MEMBERS
        public Player player;
        #endregion

        #region PRIVATE METHODS
        private void OnEnable()
        {
            if (player == null)
            {
                player = GetComponent<Player>();
            }
        }
        #endregion

    }
}