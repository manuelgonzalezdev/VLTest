using UnityEngine;
using VLTest.Utils;

namespace VLTest.Enemies.Movement
{
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