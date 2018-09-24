using UnityEngine;
using System;

namespace VLTest.Enemy.Movements
{
    public abstract class EnemyMovement : MonoBehaviour
    {
        public abstract void Move(Action callback);

    }
}