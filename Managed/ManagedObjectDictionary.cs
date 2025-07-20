using System;
using System.Collections.Generic;
using parent_house_framework.Managed.Interfaces;

namespace parent_house_framework.Managed {
    [Serializable]
    public class ManagedObjectDictionary : Dictionary<Guid, IManaged> {

        public static ManagedObjectDictionary Instance = new();
        public IManaged Get(Guid id) => this[id];
    }
}