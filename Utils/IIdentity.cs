using System;

namespace parent_house_framework.Utils {
    public interface IIdentity {
        public void CreateId();
        public void SetId(Guid id);
        public Guid GetId();
    }
}