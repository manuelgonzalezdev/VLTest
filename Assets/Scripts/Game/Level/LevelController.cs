using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using SysRandom = System.Random;
using System.Linq;
using VLTest.Commons;
using VLTest.Enemies;

namespace VLTest.Game.Level
{
    /// <summary>
    /// Main class to manage enemy spawning with a specified level config.
    /// It contains the logic to spawn enemies in an random free spawn point of the scene.
    /// </summary>
    public class LevelController : MonoBehaviour
    {
        #region MEMBERS
        public delegate void TitanEvent();
        public static event TitanEvent OnTitanSpawned;

        public Level level;
        public Scenes scenes;
        public float secondsToWaitBeforeToWin = 1f;
        public Transform player;
        public SpawnPoint[] spawnPoints;

        private bool spawning = false;
        private SpawnPoint selectedSpawnPoint;
        private int enemiesKilled;
        private bool titanSpawned;

        private bool titanMustBeSpawned
        {
            get { return enemiesKilled >= level.enemiesKilledBeforeSpawnTitan && !titanSpawned; }
        }
        #endregion

        #region PRIVATE METHODS
        private void Awake()
        {
            if (player == null)
            {
                Player.Player player = FindObjectOfType<Player.Player>();
                if(player != null)
                {
                    this.player = player.transform;
                }
                else
                {
                    Debug.LogError("Not player found");
                }
            }
        }

        private void OnEnable()
        {
            GameStateManager.OnGameStateChanges += OnGameStateChanged;
            Enemy.OnEnemyKilled += OnEnemyKilled;
            GameStateManager.currentState = GameStateManager.GameState.GAME;
        }

        private void OnDisable()
        {
            GameStateManager.OnGameStateChanges -= OnGameStateChanged;
            Enemy.OnEnemyKilled -= OnEnemyKilled;
        }

        private void OnEnemyKilled(EnemyConfig config)
        {
            enemiesKilled++;
            if (config.Equals(level.titanConfig))
            {
                StartCoroutine(WaitAndWin());
            }
        }

        private void OnGameStateChanged(GameStateManager.GameState last, GameStateManager.GameState current)
        {
            if (current == GameStateManager.GameState.GAME)
            {
                if (last != GameStateManager.GameState.PAUSE)
                {
                    InitializeLevel();
                }
                StartCoroutine(SpawnLoop());
            }
            else
            {
                StopAllCoroutines();
                if (current == GameStateManager.GameState.MENU)
                {
                    SceneManager.LoadScene(scenes.menuScene);
                }
            }
        }

        private void InitializeLevel()
        {
            level.Initialize();
            enemiesKilled = 0;
            titanSpawned = false;
        }

        private void SpawnEnemy()
        {
            if (!spawning)
            {
                StartCoroutine(FindFreeSpawnPointAndSpawn());
            }
        }

        private IEnumerator SpawnLoop()
        {
            while (Application.isPlaying)
            {
                if (!level.maxEnemiesInSceneReached)
                {
                    EnemyConfig enemyConfig = null;
                    if (titanMustBeSpawned)
                    {
                        enemyConfig = level.titanConfig;
                        titanSpawned = true;
                        if (OnTitanSpawned != null)
                        {
                            OnTitanSpawned();
                        }
                    }
                    yield return StartCoroutine(FindFreeSpawnPointAndSpawn(enemyConfig));
                }
                WaitForSeconds waiting = new WaitForSeconds(Random.Range(level.spawnFrequencyRange.x, level.spawnFrequencyRange.y));
                yield return waiting;
            }
        }

        /// <summary>
        /// Waits until a free spawn point is found and spawns an enemy on that point with a specified config
        /// </summary>
        /// <param name="enemyConfig">Config used to spawn next enemy. Null value means a random config</param>
        private IEnumerator FindFreeSpawnPointAndSpawn(EnemyConfig enemyConfig = null)
        {
            spawning = true;
            EnemyConfig config = enemyConfig == null ? level.GetRandomEnemyConfig() : enemyConfig;
            yield return StartCoroutine(GetRandomSpawnPoint(config));
            if (selectedSpawnPoint != null)
            {
                Vector3 spawnPosition = selectedSpawnPoint.getPoint();
                Vector3 playerPosition = new Vector3(player.position.x, spawnPosition.y, player.position.z);
                Quaternion rotation = Quaternion.LookRotation(playerPosition - spawnPosition);
                level.SpawnEnemy(config, spawnPosition, rotation);
            }
            spawning = false;
        }

        private IEnumerator GetRandomSpawnPoint(EnemyConfig config)
        {
            selectedSpawnPoint = null;
            WaitForSeconds waiting = new WaitForSeconds(1f);
            while (Application.isPlaying && selectedSpawnPoint == null)
            {
                SysRandom rnd = new SysRandom();
                foreach (int randomIndex in Enumerable.Range(0, spawnPoints.Length).OrderBy(x => rnd.Next()))
                {
                    SpawnPoint spawnPoint = spawnPoints[randomIndex];
                    if (spawnPoint.IsFree(config.size))
                    {
                        selectedSpawnPoint = spawnPoint;
                        break;
                    }
                }
                if (selectedSpawnPoint == null)
                {
                    yield return waiting;
                }
            }
        }

        private IEnumerator WaitAndWin()
        {
            yield return new WaitForSeconds(secondsToWaitBeforeToWin);
            GameStateManager.currentState = GameStateManager.GameState.WIN;
        }
        #endregion
    }
}


