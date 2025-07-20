using System;
using System.Collections;
using parent_house_framework.Conditions;
using Sirenix.OdinInspector;
using UnityEngine;

// Todo: Need callback for when interaction is done to optionally reset Trigger
// Todo: Need dynamic list of bool of some kind to serialize their state in between game sessions

namespace parent_house_framework.Interactions {
    public class Trigger : SerializedMonoBehaviour, IInteractable {
        [SerializeField]
        private InteractionSettings Settings;

        [SerializeField] public string TriggerName;

        
        [SerializeField, FoldoutGroup("Settings")]
        private Condition TriggerConditions;

        [SerializeField] [FoldoutGroup("Status"), ReadOnly]
        private bool m_Activated;

        private bool Activated {
            get => m_Activated;
            set {
                m_Activated = value;
                OnChangeState?.Invoke(m_Activated, Callback);
            }
        }

        [HideInInspector]
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

        public void SetState(bool state, bool instant) {
            throw new NotImplementedException();
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