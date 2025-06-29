using System;
using System.Collections.Generic;
using parent_house_framework.Managed.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace parent_house_framework.Managed {
    
    [Serializable]
    public class ManagedObjectDictionary : Dictionary<Guid, IManaged> {

        public static ManagedObjectDictionary Instance = new();
        public IManaged Get(Guid id) => this[id];
    }

    [Serializable]
    public class ManagedObject : IManaged {
        public ManagedObject() {
            Register();
        }

        ~ManagedObject() {
            Unregister();            
        }
        
        private Guid Id = Guid.NewGuid();

        public Guid SetId(Guid newId) => Id = newId;
        public Guid GetId() => Id;
        public bool CompareId(Guid otherId) => otherId == Id;
        public void Register() => ManagedObjectDictionary.Instance.TryAdd(Id, this);
        public void Unregister() => ManagedObjectDictionary.Instance.Remove(Id);
    }

    public class ManagedScriptableObject : ScriptableObject, IManaged {
        private Guid Id = Guid.NewGuid();

        public Guid SetId(Guid newId) => Id = newId;
        public Guid GetId() => Id;
        public bool CompareId(Guid otherId) => otherId == Id;
        
        // Currently unknown if register/unregister will occur on a ScriptableObject
        public void Register() => ManagedObjectDictionary.Instance.TryAdd(Id, this);
        public void Unregister() => ManagedObjectDictionary.Instance.Remove(Id);
    }

    public class ManagedGameObject : MonoBehaviour, IManaged {
        private Guid Id = Guid.NewGuid();

        public Guid SetId(Guid newId) => Id = newId;
        public Guid GetId() => Id;
        public bool CompareId(Guid otherId) => otherId == Id;
        public void Register() => ManagedObjectDictionary.Instance.TryAdd(Id, this);
        public void Unregister() => ManagedObjectDictionary.Instance.Remove(Id);

        protected virtual void OnEnable() {
            Register();
        }

        protected virtual void OnDisable() {
            Unregister();
        }
    }

    public class SerializedManagedGameObject : SerializedMonoBehaviour, IManaged {
        private Guid Id = Guid.NewGuid();

        public Guid SetId(Guid newId) => Id = newId;
        public Guid GetId() => Id;
        public bool CompareId(Guid otherId) => otherId == Id;
        public void Register() => ManagedObjectDictionary.Instance.TryAdd(Id, this);
        public void Unregister() => ManagedObjectDictionary.Instance.Remove(Id);

        protected virtual void OnEnable() {
            Register();
        }

        protected virtual void OnDisable() {
            Unregister();
        }
    }
}