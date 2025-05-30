using System;
using System.Collections;
using parent_house_framework.Conditions;
using Sirenix.OdinInspector;
using UnityEngine;

// Todo: Need callback for when interaction is done to optionally reset Trigger
// Todo: Need dynamic bools of some kind to serialize their state in between game sessions

namespace parent_house_framework.Interactions {
    public class Trigger : SerializedMonoBehaviour, IInteractable {
        [SerializeField, FoldoutGroup("Settings")]
        private InteractionSettings Settings;

        [SerializeField, FoldoutGroup("Settings")]
        private Condition TriggerConditions;

        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private bool m_Activated;

        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private bool m_ReadyToTrigger; // Prevents state change onEnable

        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private bool m_Instant;

        public bool IsReady => m_ReadyToTrigger;

        public bool Activated {
            get => m_Activated;
            private set {
                if (!m_ReadyToTrigger) {
                    m_ReadyToTrigger = true;
                }
                else {
                    m_Activated = value;

                    OnChangeState?.Invoke(m_Activated, m_Instant, Callback);
                }
            }
        }

        // [SerializeField] [FoldoutGroup("Events")]
        public Action<bool, bool, Action> OnChangeState;

        private void Callback() {
            if (Settings.ResetOnceFinished) {
                ChangeState();
            }
        }

        private void OnEnable() {
            StartCoroutine(Delay());

            IEnumerator Delay() {
                yield return new WaitForEndOfFrame();
                m_ReadyToTrigger = Settings.ReadyOnEnable;
                Activated = Settings.ActiveOnEnable;
                m_Instant = Settings.InstantOnEnable;
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
            m_Instant = false;
            Activated = state;
        }

        public void SetState(bool state, bool instant) {
            m_Instant = instant;
            Activated = state;
        }
    }
}