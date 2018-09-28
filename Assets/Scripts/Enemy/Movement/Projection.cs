using UnityEngine;
using VLTest.Utils;

namespace VLTest.Enemies.Movement
{
    /// <summary>
    /// A projection is an area reserved for this enemy to avoid overlaping between enemies.
    /// It's managed by current EnemyMovementLogic script
    /// </summary>
    [RequireComponent(typeof(BoxCollider))]
    public class Projection : ObjectPoolItem
    {

        public override void Deactivate()
        {
            transform.localScale = Vector3.one;
            base.Deactivate();
        }

    }
}