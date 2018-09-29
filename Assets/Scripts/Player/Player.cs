using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Player.Cameras;
using VLTest.Game;

namespace VLTest.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerInput input;
        public PlayerCameraController cameraController;
        public PlayerHealth playerHealth;
        public PlayerAttack attack;
        public PlayerStats stats;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
            cameraController = GetComponent<PlayerCameraController>();
            attack = GetComponent<PlayerAttack>();
        }

        public void Kill()
        {
            GameStateManager.currentState = GameStateManager.GameState.LOSE;
        }

    }
}