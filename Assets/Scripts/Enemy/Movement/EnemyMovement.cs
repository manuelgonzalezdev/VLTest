using UnityEngine;
using System.Collections.Generic;

namespace VLTest.Enemies.Movement
{
    public class EnemyMovement : MonoBehaviour
    {
        public bool moving;
        public float speed;

        EnemyMovementLogic mainMovement;
        List<EnemyMovementLogic> secondaryMovements;

        public void LoadMovements(EnemyConfig config)
        {
            speed = config.speed;
            mainMovement = config.mainMovement.CreateMovement(gameObject);
            secondaryMovements = new List<EnemyMovementLogic>();
            foreach (EnemyMovementType movementType in config.secondayMovements)
            {
                secondaryMovements.Add(movementType.CreateMovement(gameObject));
            }
        }

        private void OnEnable()
        {
            Move();
        }

        void Move()
        {
            mainMovement.Move(speed, Move);

        }

    }

}