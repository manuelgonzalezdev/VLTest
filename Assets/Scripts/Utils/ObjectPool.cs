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

        [System.NonSerialized] private List<ObjectPoolItem> inactiveItems = new List<ObjectPoolItem>();
        [System.NonSerialized] private List<ObjectPoolItem> activeItems = new List<ObjectPoolItem>();
        [System.NonSerialized] private bool populated = false;
        [System.NonSerialized] private long lastID = 0;

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
                inactiveItems.Remove(item);
                if (inactiveItems.Contains(item))
                {
                    Debug.LogError("Ding!");
                }
            }
            else
            {
                item = Create();
            }

            if (item == null)
            {
                Debug.LogError("Ding!");
            }

            activeItems.Add(item);
            item.gameObject.transform.SetPositionAndRotation(position, rotation);
            item.Activate();

            return item;
        }

        ObjectPoolItem Create()
        {
            GameObject gO = GameObject.Instantiate(itemPrefab);
            gO.name = gO.name.Replace("(Clone)", "_" + (++lastID));
            ObjectPoolItem item = Utils.GetComponentOrCreateIfNotExists<ObjectPoolItem>(gO);
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
                if (inactiveItems.Contains(item))
                {
                    Debug.LogError("Ding!");
                }
                inactiveItems.Add(item);
                item.gameObject.SetActive(false);
                item.transform.SetPositionAndRotation(inactivePosition, Quaternion.identity);
            }
            else
            {
                GameObject.Destroy(item.gameObject);
            }
            if (ItemNull())
            {
                Debug.LogError("Ding!");
            }
        }

        bool ItemNull()
        {
            foreach (ObjectPoolItem item in inactiveItems)
            {
                if(item == null)
                {
                    return true;
                }
            }
            return false;
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