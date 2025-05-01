using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

// Todo: Need callback for when interaction is done to optionally reset Trigger
// Todo: Need dynamic bools of some kind to serialize their state in between game sessions

namespace Parent_House_Framework.Interactions {
    public class Trigger : SerializedMonoBehaviour, IInteractable {
        [SerializeField, FoldoutGroup("Settings")]
        private InteractionSettings Settings;

        [SerializeField, FoldoutGroup("Settings")]
        private Condition TriggerConditions;

        [SerializeField] [FoldoutGroup("Status"), ReadOnly]
        private bool m_Activated;
        
        public bool Activated {
            get => m_Activated;
            private set {
                m_Activated = value;
                OnChangeState?.Invoke(m_Activated, Callback);
            }
        }

        // [SerializeField] [FoldoutGroup("Events")]
        public Action<bool, Action> OnChangeState;

        private void Callback() {
            if (Settings.ResetOnceFinished) {
                ChangeState();
                Debug.Log("Changing states");
            }
        }

        private void OnEnable() {
            StartCoroutine(Delay());

            IEnumerator Delay() {
                yield return new WaitForEndOfFrame();
                Activated = Settings.ActiveOnEnable;
            }
        }

        public void Notify(NotifyState state) {
            if (RequireButtonToChangeState()) return;
            ChangeState();
        }

        public bool RequireButtonToChangeState() {
            return Settings.RequireKeyToActivate;
        }

        [Button]
        public void ChangeState() {
            if (TriggerConditions != null && !TriggerConditions.IsConditionMet())
                return;
            if (Settings.Toggles) {
                Activated = !Activated;
            }
            else {
                if (Activated && !Settings.AlwaysTriggers)
                    return;
                Activated = true;
            }
        }

        public void SetState(bool state) {
            if (Activated != state)
                Activated = state;
        }
    }
}