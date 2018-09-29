using UnityEngine;

namespace VLTest.Game
{

    public class TimeManager : MonoBehaviour
    {
        private void OnEnable()
        {
            GameStateManager.OnGameStateChanges += OnGameStateChanged;
        }

        private void OnDisable()
        {
            GameStateManager.OnGameStateChanges -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameStateManager.GameState old, GameStateManager.GameState current)
        {
            bool gameStopped = current == GameStateManager.GameState.LOSE || current == GameStateManager.GameState.WIN || current == GameStateManager.GameState.PAUSE;
            float timeScale = gameStopped ? 0 : 1;
            SetTimeScale(timeScale);
        }

        private void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
        }
    }
}