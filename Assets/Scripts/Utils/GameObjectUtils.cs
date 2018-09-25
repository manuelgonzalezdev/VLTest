using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Utils
{
    public static class GameObjectUtils
    {
        public static T GetComponentOrCreateIfNotExists<T>(GameObject gO) where T : Component
        {
            T component  = gO.GetComponent<T>();
            if(component == null)
            {
                component = gO.AddComponent<T>();
            }
            return component;
        }
    }

}