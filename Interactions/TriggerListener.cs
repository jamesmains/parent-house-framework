using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace parent_house_framework.Interactions {
    public class TriggerListener : MonoBehaviour {
        [SerializeField, FoldoutGroup("Dependencies")]
        public Trigger Trigger;

        [SerializeField, FoldoutGroup("Dependencies")]
        [ValueDropdown(nameof(GetAllTriggerEffects), IsUniqueList = true)]
        public TriggerEffect[] BehaviorEffects;

        private IEnumerable<ValueDropdownItem> GetAllTriggerEffects() {
            return FindObjectsOfType<TriggerEffect>()
                .Select(effect => new ValueDropdownItem($"{effect.customName} ({effect.gameObject.name})", effect));
        }

        private void OnEnable() {
            Trigger.OnChangeState += Handle;
        }

        private void OnDisable() {
            Trigger.OnChangeState -= Handle;
        }

        private void Handle(bool state, bool instant, Action callback) {
            foreach (var target in BehaviorEffects) {
                target.HandleStateChange(state, instant, callback);
            }
        }
    }
}