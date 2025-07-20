using System;
using parent_house_framework.Managed.Interfaces;
using UnityEngine;

namespace parent_house_framework.Managed {
    public class ManagedScriptableObject : ScriptableObject, IManaged {
        private Guid Id = Guid.NewGuid();

        public Guid SetId(Guid newId) => Id = newId;
        public Guid GetId() => Id;
        public bool CompareId(Guid otherId) => otherId == Id;
        
        // Currently unknown if register/unregister will occur on a ScriptableObject
        public void Register() => ManagedObjectDictionary.Instance.TryAdd(Id, this);
        public void Unregister() => ManagedObjectDictionary.Instance.Remove(Id);
    }
}