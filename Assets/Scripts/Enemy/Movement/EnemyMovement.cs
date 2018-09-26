using UnityEngine;
using System.Collections.Generic;

namespace VLTest.Enemies.Movement
{
    public class EnemyMovement : MonoBehaviour
    {
        public float speed;
        public int secondaryMovementFrequency;

        private int currentSteps;
        private EnemyMovementLogic currentMovement;

        private EnemyMovementLogic mainMovement;
        private List<EnemyMovementLogic> secondaryMovements;

        public void LoadMovements(EnemyConfig config)
        {
            speed = config.speed;
            secondaryMovementFrequency = config.secondaryMovementFrequency;
            mainMovement = config.mainMovement.CreateMovement(gameObject);
            secondaryMovements = new List<EnemyMovementLogic>();
            foreach (EnemyMovementType movementType in config.secondayMovements)
            {
                secondaryMovements.Add(movementType.CreateMovement(gameObject));
            }
        }

        public void Play()
        {
            currentSteps = 0;
            Move();
        }

        public void Stop()
        {
            if (currentMovement != null)
            {
                currentMovement.Cancel();
            }
        }

        void Move()
        {
            if (currentSteps == secondaryMovementFrequency && secondaryMovements.Count > 0)
            {
                currentSteps = 0;
                int secondaryIndex = Random.Range(0, secondaryMovements.Count);
                currentMovement = secondaryMovements[secondaryIndex];
            }
            else
            {
                currentMovement = mainMovement;
            }
            currentMovement.Move(speed, OnMovementFinished);
        }

        void OnMovementFinished()
        {
            currentSteps++;
            Move();
        }

    }

}