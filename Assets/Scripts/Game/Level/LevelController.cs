using System.Collections;
using UnityEngine;
using SysRandom = System.Random;
using System.Linq;
using VLTest.Game;
using VLTest.Enemies;

namespace VLTest.Level
{
    /// <summary>
    /// Main class to manage enemy spawning with a specified level config.
    /// It contains the logic to spawn enemies in an random free spawn point of the scene.
    /// </summary>
    public class LevelController : MonoBehaviour
    {

        public Level level;
        public Transform player;
        public SpawnPoint[] spawnPoints;

        private bool spawning = false;
        private SpawnPoint selectedSpawnPoint;

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
            // TEMPORAL
            GameStateManager.currentState = GameStateManager.GameState.GAME;
        }

        private void OnDisable()
        {
            GameStateManager.OnGameStateChanges -= OnGameStateChanged;
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
            }
        }

        private void InitializeLevel()
        {
            level.Initialize();
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
            WaitForSeconds waiting = new WaitForSeconds(level.spawnFrequencyInSeconds);
            while (Application.isPlaying)
            {
                if (!level.maxEnemiesInSceneReached)
                {
                    yield return StartCoroutine(FindFreeSpawnPointAndSpawn());
                }
                yield return waiting;
            }
        }

        private IEnumerator FindFreeSpawnPointAndSpawn()
        {
            spawning = true;
            EnemyConfig config = level.GetRandomEnemyConfig();
            yield return StartCoroutine(GetRandomSpawnPoint(config));
            if (selectedSpawnPoint != null)
            {
                Vector3 position = selectedSpawnPoint.getPoint();
                Quaternion rotation = Quaternion.LookRotation(player.position - position);
                level.SpawnEnemy(config, position, rotation);
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

    }
}


