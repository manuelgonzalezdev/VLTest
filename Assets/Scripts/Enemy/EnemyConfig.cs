using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Enemy.Movement;

namespace VLTest.Enemy
{
    [CreateAssetMenu(menuName = "Enemy/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        public float life = 1;
        public float size = 1;
        public float speed = 1;

        public EnemyMovementType mainMovement;
        public List<EnemyMovementType> secondayMovements;

    }
}