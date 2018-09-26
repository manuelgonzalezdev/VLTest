using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Utils
{
    [CreateAssetMenu(menuName = "Object Pool")]
    public class ObjectPool : ScriptableObject
    {
        public GameObject itemPrefab;
        public int minPopulation = 5;
        public int maxPopulation = 10;
        public Vector3 inactivePosition = new Vector3(9999, 9999, 9999);

        private List<ObjectPoolItem> inactiveItems = new List<ObjectPoolItem>();
        private List<ObjectPoolItem> activeItems = new List<ObjectPoolItem>();

        private bool populated = false;

        public int activeItemsCount
        {
            get { return activeItems.Count; }
        }

        public void Populate()
        {
            Clear();
            for (int i = 0; i < minPopulation; i++)
            {
                inactiveItems.Add(Create());
            }
            populated = true;
        }

        public ObjectPoolItem Spawn(Vector3 position, Quaternion rotation)
        {
            if (!populated)
            {
                Populate();
            }

            ObjectPoolItem item = null;
            if (inactiveItems.Count > 0)
            {
                item = inactiveItems[0];
                inactiveItems.RemoveAt(0);
            }
            else
            {
                item = Create();
            }

            activeItems.Add(item);
            item.gameObject.transform.SetPositionAndRotation(position, rotation);
            item.Activate();

            return item;
        }

        ObjectPoolItem Create()
        {
            GameObject gO = GameObject.Instantiate(itemPrefab);
            ObjectPoolItem item = GameObjectUtils.GetComponentOrCreateIfNotExists<ObjectPoolItem>(gO);
            item.Initialize(this);
            gO.SetActive(false);
            gO.transform.SetPositionAndRotation(inactivePosition, Quaternion.identity);
            return item;
        }

        public void DestroyItem(ObjectPoolItem item)
        {
            activeItems.Remove(item);
            if (inactiveItems.Count < maxPopulation)
            {
                inactiveItems.Add(item);
                item.gameObject.SetActive(false);
                item.transform.SetPositionAndRotation(inactivePosition, Quaternion.identity);
            }
            else
            {
                GameObject.Destroy(item.gameObject);
            }

        }

        public void Clear()
        {
            if (activeItems.Count > 0)
            {
                for (int i = 0; i < activeItems.Count; i++)
                {
                    if (activeItems[i] != null)
                    {
                        GameObject.Destroy(activeItems[i].gameObject);
                    }
                }
                activeItems.Clear();

            }
            if (inactiveItems.Count > 0)
            {
                for (int i = 0; i < inactiveItems.Count; i++)
                {
                    if (inactiveItems[i] != null)
                    {
                        GameObject.Destroy(inactiveItems[i].gameObject);
                    }
                }
                inactiveItems.Clear();
            }
        }

    }

}