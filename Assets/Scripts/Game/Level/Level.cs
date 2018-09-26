using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Enemies;
using VLTest.Utils;

namespace VLTest.Level
{
    [CreateAssetMenu(menuName = "Level")]
    public class Level : ScriptableObject
    {
        public float spawnFrequencyInSeconds = 2;
        public int maxEnemiesInScene = 20;

        public ObjectPool enemyPool;

        public List<EnemyConfig> enemyConfigs;
        public EnemyConfig titanConfig;

        public bool maxEnemiesInSceneReached
        {
            get { return enemyPool.activeItemsCount >= maxEnemiesInScene; }
        }

        public void Initialize()
        {
            enemyConfigs.Sort(SortBySpawnProbability);
            enemyPool.Populate();
        }

        public Enemy SpawnEnemy(Vector3 position, Quaternion rotation)
        {
            Enemy enemy = enemyPool.Spawn(position, rotation).GetComponent<Enemy>();
            enemy.LoadConfig(GetRandomEnemyConfig());
            return enemy;
        }

        private EnemyConfig GetRandomEnemyConfig()
        {
            float probability = Random.Range(0f, 1f);
            for (int i = 0; i < enemyConfigs.Count; i++)
            {
                EnemyConfig config = enemyConfigs[i];
                if (config.spawnProbability >= probability)
                {
                    return config;
                }
            }
            return enemyConfigs[enemyConfigs.Count - 1];
        }

        private int SortBySpawnProbability(EnemyConfig config1, EnemyConfig config2)
        {
            return config1.spawnProbability.CompareTo(config2.spawnProbability);
        }

    }
}