using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Game
{
    public class HideMouse : MonoBehaviour
    {

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
            bool hideMouse = newState == GameStateManager.GameState.GAME;
            SetCursorVisibility(hideMouse);
        }
    
        private void SetCursorVisibility(bool hide)
        {
            Cursor.visible = !hide;
        }

    }
}