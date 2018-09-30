using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Enemies.Movement;

namespace VLTest.Enemies
{
    [CreateAssetMenu(menuName = "Enemy/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        #region MEMBERS
        public float health = 1f;
        public float damage = 1f;
        public float size = 1f;
        public float speed = 1f;
        public int secondaryMovementFrequency = 3;
        [Range(0f, 1f)]
        public float spawnProbability = 0.5f;

        public EnemyMovementType mainMovement;
        public List<EnemyMovementType> secondayMovements;
        #endregion

    }
}