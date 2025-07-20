using System;
using System.Collections.Generic;
using System.Linq;
using parent_house_framework.Cores;
using Sirenix.OdinInspector;
using UnityEngine;

namespace parent_house_framework.Interactions {
    public class TriggerListener : SerializedMonoBehaviour {
        [ValueDropdown(nameof(GetAllTriggers), IsUniqueList = true)]
        [SerializeField, FoldoutGroup("Dependencies")]
        public Trigger Trigger;

        [SerializeField, FoldoutGroup("Settings")]
        public readonly List<ObjectEffect> Effects = new();
        
        private IEnumerable<ValueDropdownItem> GetAllTriggers() {
            return FindObjectsByType<Trigger>(FindObjectsInactive.Include,FindObjectsSortMode.InstanceID)
                .Select(t => new ValueDropdownItem($"{t.TriggerName} ({t.gameObject.name})", t));
        }
        
        private void Awake() {
            Initialize();
        }
    
#if UNITY_EDITOR
        private void OnValidate() {
            Initialize();
        }
#endif
        
        private void OnEnable() {
            Trigger.OnChangeState += Handle;
        }

        private void OnDisable() {
            Trigger.OnChangeState -= Handle;
        }

        private void Initialize() {
            foreach (var behavior in Effects) {
                behavior.Initialize(this.gameObject);
            }
        }
        
        private void Handle(bool state, Action action) {
            foreach (var target in Effects) {
                target.HandleState(state, action);
            }
        }
    }
}