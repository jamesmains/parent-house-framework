using System;
using parent_house_framework.Managed.Interfaces;

namespace parent_house_framework.Managed {
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
}