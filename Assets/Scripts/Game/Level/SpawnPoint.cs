using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Game
{
    public class SpawnPoint : MonoBehaviour
    {
        #region MEMBERS
        public string spawnPointLayer = "SpawnPoint";
        public string enemyLayer = "Enemy";
        #endregion

        #region PUBLIC METHODS
        public bool IsFree(float radius)
        {
            return Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask(enemyLayer)).Length == 0;
        }

        public Vector3 getPoint()
        {
            return transform.position;
        }
        #endregion

        #region PRIVATE METHODS
        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer(spawnPointLayer);
        }
        #endregion
    }
}