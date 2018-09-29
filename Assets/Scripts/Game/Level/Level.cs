using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Enemies;
using VLTest.Utils;

namespace VLTest.Level
{
    /// <summary>
    /// A Level contains an enemy spawning config and the logic to spawn them
    /// </summary>
    /// 
    [CreateAssetMenu(menuName = "Level")]
    public class Level : ScriptableObject
    {
        /// <summary>
        /// Spawn frequency (in seconds) is a random value between a minimum (x) and maximum (y) value.
        /// This random value will be recalculated when an enemy is spawned.
        /// </summary>
        [Tooltip("Spawn frequency (in seconds) is a random value between a minimum (x) and maximum (y) value. This random value will be recalculated when an enemy is spawned.")]
        public Vector2 spawnFrequencyRange = new Vector2(1f, 4f);
        public int maxEnemiesInScene = 20;
        public int enemiesKilledBeforeSpawnTitan = 30;
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

        public void ClearLevel()
        {
            enemyPool.Clear();
        }

        public Enemy SpawnEnemy(EnemyConfig enemyConfig, Vector3 position, Quaternion rotation)
        {
            Vector3 finalPosition = position;
            finalPosition.y -= 0.5f - (enemyConfig.size * 0.5f);
            Enemy enemy = enemyPool.Spawn(finalPosition, rotation).GetComponent<Enemy>();
            enemy.LoadConfig(enemyConfig);
            enemy.MoveTowards();
            return enemy;
        }

        public EnemyConfig GetRandomEnemyConfig()
        {
            for (int i = 0; i < enemyConfigs.Count; i++)
            {
                EnemyConfig config = enemyConfigs[i];
                bool isLastElement = i == (enemyConfigs.Count - 1);
                if (isLastElement)
                {
                    return config;
                }
                else if (config.spawnProbability >= Random.Range(0f, 1f))
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