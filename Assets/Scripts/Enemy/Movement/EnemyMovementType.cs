using UnityEngine;
using System;
using VLTest.Utils;

namespace VLTest.Enemies.Movement
{
    public abstract class EnemyMovementType : ScriptableObject
    {
        public abstract EnemyMovementLogic CreateMovement(GameObject enemy);
    }

    /// <summary>
    /// Parent class to define differents types of movement.
    /// Handles movement's projections and callbacks.
    /// </summary>
    public abstract class EnemyMovementLogic
    {
        protected const string ENEMY_LAYER = "Enemy";
        protected const string PROJECTION_LAYER= "Projection";

        protected Transform transform;
        protected Action callback;
        protected Projection projection;
        protected int overlapMask;

        public EnemyMovementLogic(GameObject enemy)
        {
            transform = enemy.transform;
            overlapMask = LayerMask.GetMask("Enemy", "Projection");
        }

        public abstract bool Move(float speed, Action callback, ObjectPool projectionPool);

        public virtual void Cancel()
        {
            if (projection != null)
            {
                projection.Deactivate();
            }
        }

        protected Projection CreateProjection(ObjectPool projectionPool, Vector3 direction)
        {
            Vector3 localDirection = transform.TransformDirection(direction);
            float size = transform.localScale.z;

            Projection projection = (Projection)projectionPool.Spawn(transform.position, transform.rotation);

            projection.transform.position = transform.position;
            projection.transform.position += localDirection * (size * 0.5f);
            projection.transform.localScale = (Vector3.one * size) + (Utils.Utils.Abs(direction) * size);

            return projection;
        }

        protected bool MovementIsValid(Vector3 destination, float size, Quaternion orientation)
        {
            return Physics.OverlapBox(destination, Vector3.one * (size * 0.49f), orientation, overlapMask).Length == 0;
        }

        protected virtual void OnMovementFinished()
        {
            this.projection.Deactivate();
            if (callback != null)
            {
                callback.Invoke();
            }
        }

    }
}