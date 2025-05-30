using System;
using System.Collections.Generic;
using parent_house_framework.Cores;
using Sirenix.OdinInspector;
using UnityEngine;

namespace parent_house_framework.Interactions {
    public class TriggerEffect : SerializedMonoBehaviour {
        [Title("Effect Name")]
        public string customName = "Default Name";

        [SerializeField, FoldoutGroup("Settings")]
        public readonly List<ObjectEffect> Behaviors = new();

        private void Awake() {
            Initialize();
        }

#if UNITY_EDITOR
        private void OnValidate() {
            Initialize();
        }
#endif

        private void Initialize() {
            foreach (var behavior in Behaviors) {
                behavior.Initialize(this.gameObject);
            }
        }

        public void HandleStateChange(bool state, bool instant, Action callback) {
            foreach (var behavior in Behaviors) {
                behavior.HandleState(state, instant, callback);
            }
        }
    }
}