using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Utils
{
    public class ObjectPoolItem : MonoBehaviour
    {
        private ObjectPool pool;

        public void Initialize(ObjectPool pool)
        {
            this.pool = pool;
        }

        [ContextMenu("Destroy")]
        public virtual void Deactivate()
        {
            pool.DestroyItem(this);
        }

    }

}
