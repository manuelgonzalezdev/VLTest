using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Game;

namespace VLTest.Level
{
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
                Player player = FindObjectOfType<Player>();
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
            yield return StartCoroutine(GetRandomSpawnPoint());
            if (selectedSpawnPoint != null)
            {
                Vector3 position = selectedSpawnPoint.getPoint();
                Quaternion rotation = Quaternion.LookRotation(player.position - position);
                level.SpawnEnemy(position, rotation);
            }
            spawning = false;
        }

        private IEnumerator GetRandomSpawnPoint()
        {
            selectedSpawnPoint = null;
            WaitForSeconds waiting = new WaitForSeconds(1f);
            while (Application.isPlaying && selectedSpawnPoint == null)
            {
                foreach (SpawnPoint spawnPoint in spawnPoints)
                {
                    if (spawnPoint.IsFree())
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


