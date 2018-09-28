using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Player.Cameras;

namespace VLTest.Player
{
    public class Player : MonoBehaviour
    {

        public PlayerInput input;
        public PlayerCameraController cameraController;
        public PlayerAttack attack;
        public PlayerStats stats;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
            cameraController = GetComponent<PlayerCameraController>();
            attack = GetComponent<PlayerAttack>();
        }

    }
}