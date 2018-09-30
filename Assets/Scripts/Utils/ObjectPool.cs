using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Utils
{
    [CreateAssetMenu(menuName = "Object Pool")]
    public class ObjectPool : ScriptableObject
    {
        #region MEMBERS
        public GameObject itemPrefab;
        public int minPopulation = 5;
        public int maxPopulation = 10;
        public Vector3 inactivePosition = new Vector3(9999, 9999, 9999);

        [System.NonSerialized] private List<ObjectPoolItem> inactiveItems = new List<ObjectPoolItem>();
        [System.NonSerialized] private List<ObjectPoolItem> activeItems = new List<ObjectPoolItem>();
        [System.NonSerialized] private Transform inactiveParent;
        [System.NonSerialized] private bool populated = false;
        [System.NonSerialized] private long lastID = 0;

        public int activeItemsCount
        {
            get { return activeItems.Count; }
        }

        #endregion

        #region PUBLIC METHODS

        public void Populate()
        {
            inactiveParent = CreateInactiveParent(inactivePosition);
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

        #endregion

        #region PRIVATE METHODS

        private Transform CreateInactiveParent(Vector3 position)
        {
            Transform inactiveParent = new GameObject().transform;
            inactiveParent.name = string.Concat(itemPrefab.name, " Pool");
            inactiveParent.position = inactivePosition;
            return inactiveParent;
        }

        private ObjectPoolItem Create()
        {
            GameObject gO = GameObject.Instantiate(itemPrefab);
            string nameSuffix = string.Concat("_" + (++lastID));
            gO.name = gO.name.Replace("(Clone)", nameSuffix);
            ObjectPoolItem item = Utils.GetComponentOrCreateIfNotExists<ObjectPoolItem>(gO);
            item.Initialize(this);
            gO.SetActive(false);
            gO.transform.SetParent(inactiveParent);
            gO.transform.SetPositionAndRotation(inactivePosition, Quaternion.identity);
            return item;
        }
        #endregion
    }

}