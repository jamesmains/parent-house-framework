using System;
using System.Collections.Generic;
using System.Linq;
using parent_house_framework.Managed;
using Sirenix.Utilities;
using UnityEngine;

namespace parent_house_framework.Values {
    /// <summary>
    /// I actually kind of don't like these.
    /// </summary>
    public class BindingBool : ManagedObject {
        public BindingBool(string name,ref bool startingValue) {
            Name = name;
            Value = startingValue;
        }
        public string Name;
        public bool Value;

        public static bool TrueForAll(Dictionary<Guid, BindingBool> bindings) {
            if (bindings == null || bindings.Count == 0) return true;
            foreach (var binding in bindings) {
                Debug.Log($"{binding.Value.Name} {binding.Value.Value}");
            }
            Debug.Log($"All values true? {bindings.All(binding => binding.Value.Value)}");
            return bindings.All(binding => binding.Value.Value);
        }
    }
}