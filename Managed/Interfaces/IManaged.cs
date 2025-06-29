using System;

namespace parent_house_framework.Managed.Interfaces {
    public interface IManaged {
        public Guid SetId(Guid newId);
        public Guid GetId();
        public bool CompareId(Guid otherId);
        public void Register();
        public void Unregister();
    }
}