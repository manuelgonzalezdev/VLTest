using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Utils;

namespace VLTest.Enemy
{
    public class EnemyPool : MonoBehaviour
    {
        public GameObject enemyPrefab;

        ObjectPool enemyPool;

        private void Start()
        {
            enemyPool = new ObjectPool(enemyPrefab, 5, 10);
            enemyPool.Populate();
        }

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            enemyPool.Spawn(Vector3.one * Random.Range(0, 20), Quaternion.identity);
        }

    }
}