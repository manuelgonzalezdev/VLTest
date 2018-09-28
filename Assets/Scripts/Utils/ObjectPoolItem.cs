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

        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        [ContextMenu("Destroy")]
        public virtual void Deactivate()
        {
            if (pool != null)
            {
                pool.DestroyItem(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

    }

}
