using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Enemies.Movement;

namespace VLTest.Enemies
{
    [CreateAssetMenu(menuName = "Enemy/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        public float life = 1f;
        public float size = 1f;
        public float speed = 1f;
        [Range(0f, 1f)]
        public float spawnProbability = 0.5f;

        public EnemyMovementType mainMovement;
        public List<EnemyMovementType> secondayMovements;

    }
}