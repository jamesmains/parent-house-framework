using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace parent_house_framework.Managed.Debug {
    public class DebugManagedObjects : MonoBehaviour {
        private List<DebugManagedObject> DebugObjects = new List<DebugManagedObject>();

        #region Methods

        [Button]
        public void Debug_Create() {
            DebugObjects.Add(new DebugManagedObject());
            UnityEngine.Debug.Log($"New Count: {DebugObjects.Count}");

        }

        [Button]
        public void Debug_UseDebugObject() {
            var remainingDebugObjects = DebugObjects;
            if (DebugObjects.Count > 0)
            {
                if (DebugObjects[0].Use())
                {
                    remainingDebugObjects.Remove(DebugObjects[0]);
                }
            }
            UnityEngine.Debug.Log($"Original Count: {DebugObjects.Count}");
            DebugObjects = remainingDebugObjects;
            UnityEngine.Debug.Log($"Remaining Count: {DebugObjects.Count}");
        }

        #endregion
        
    }

    [Serializable]
    public class DebugManagedObject : ManagedObject {
        public int Uses = 5;

        public bool Use() {
            UnityEngine.Debug.Log($"Uses: {Uses}");
            Uses--;
            UnityEngine.Debug.Log($"Uses Remaining: {Uses}");
            return Uses <= 0;
        }
    }
}