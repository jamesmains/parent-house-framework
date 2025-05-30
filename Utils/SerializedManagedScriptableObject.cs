using System;
using Sirenix.OdinInspector;

namespace parent_house_framework.Utils {
    public abstract class SerializedManagedScriptableObject: SerializedScriptableObject, IIdentity {
        public SerializedManagedScriptableObject() {
            CreateId();
        }
        [FoldoutGroup("Identity"),ReadOnly]
        public Guid Id;

        public void SetId() {
            
        }

        public virtual void CreateId() {
            Id = Guid.NewGuid();
        }
        
        public virtual void SetId(Guid id) {
            Id = id == Guid.Empty ? Id : id;
        }

        public virtual Guid GetId() {
            return Id;
        }
        
        public bool HasSameId(Guid id) => Id == id;
    }
}