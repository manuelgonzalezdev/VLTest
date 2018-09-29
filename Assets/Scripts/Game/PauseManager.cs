using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Game;

namespace VLTest.UI
{

    public class PauseManager : MonoBehaviour
    {

        public string pauseInput = "Pause";

        private bool pauseDown;

        private void Update()
        {
            bool pausePressed = Input.GetAxis(pauseInput) != 0;

            if (!pauseDown && pausePressed)
            {
                Pause();
                pauseDown = true;
            }
            if (pauseDown && !pausePressed)
            {
                pauseDown = false;
            }
        }

        private void Pause()
        {
            GameStateManager.currentState = GameStateManager.GameState.PAUSE;
        }

    }
}