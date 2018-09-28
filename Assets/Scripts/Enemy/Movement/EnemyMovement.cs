using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VLTest.Utils;

namespace VLTest.Enemies.Movement
{
    /// <summary>
    /// Handles enemy movement logic.
    /// There're two kind of movements: main (only one) and secondary(one or more). 
    /// Main movement is executed by default. Secondaries are executed when main movement
    /// has been performed a determined amount of times (steps).
    /// </summary>
    public class EnemyMovement : MonoBehaviour
    {
        public float speed;
        public int secondaryMovementFrequency;
        public ObjectPool projectionPool;

        private EnemyMovementLogic mainMovement;
        private List<EnemyMovementLogic> secondaryMovements;

        private int currentSteps;
        private EnemyMovementLogic currentMovement;

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

        private void Move()
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
            bool valid = currentMovement.Move(speed, OnMovementFinished, projectionPool);

            if (!valid)
            {
                StartCoroutine(WaitAndTryToMoveAgain());
            }
        }

        private void OnMovementFinished()
        {
            currentSteps++;
            Move();
        }

        private IEnumerator WaitAndTryToMoveAgain()
        {
            yield return new WaitForSeconds(1f);
            Move();
        }

    }

}