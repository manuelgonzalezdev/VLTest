using UnityEngine;
using VLTest.Game;

namespace VLTest.UI
{

    public class UIGame : MonoBehaviour
    {

        public GameObject gamePanel;
        public GameObject pausePanel;
        public GameObject finishPanel;

        public void Resume()
        {
            GameStateManager.currentState = GameStateManager.GameState.GAME;
        }

        public void ToMenu()
        {
            GameStateManager.currentState = GameStateManager.GameState.MENU;
        }

        private void OnEnable()
        {
            GameStateManager.OnGameStateChanges += OnGameStateChanged;
        }

        private void OnDisable()
        {
            GameStateManager.OnGameStateChanges -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameStateManager.GameState old, GameStateManager.GameState newState)
        {
            gamePanel.SetActive(newState == GameStateManager.GameState.GAME);
            pausePanel.SetActive(newState == GameStateManager.GameState.PAUSE);
            finishPanel.SetActive(newState == GameStateManager.GameState.LOSE || newState == GameStateManager.GameState.WIN);
        }

    }
}