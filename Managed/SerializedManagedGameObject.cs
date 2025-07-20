using System;
using parent_house_framework.Managed.Interfaces;
using Sirenix.OdinInspector;

namespace parent_house_framework.Managed {
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