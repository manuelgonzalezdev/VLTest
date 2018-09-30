using UnityEngine;

namespace VLTest.Utils
{
    public static class Utils
    {
        #region PUBLIC METHODS
        public static T GetComponentOrCreateIfNotExists<T>(GameObject gO) where T : Component
        {
            T component = gO.GetComponent<T>();
            if (component == null)
            {
                component = gO.AddComponent<T>();
            }
            return component;
        }

        public static Vector3 Abs(Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }
    }
    #endregion
}