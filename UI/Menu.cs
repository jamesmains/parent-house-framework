using System.Collections;
using parent_house_framework.Conditions;
using parent_house_framework.Interactions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace parent_house_framework.UI {
    public enum MenuState {
        Closed,
        Open
    }

    public class MenuStateCondition : Condition {
        [SerializeField, BoxGroup("Dependencies")]
        private Menu TargetMenu;

        public override bool IsConditionMet() {
            return TargetMenu.State == MenuState.Open;
        }
    }

    [RequireComponent(typeof(CanvasGroup))]
    public class Menu : SerializedMonoBehaviour {
        [SerializeField, FoldoutGroup("Settings")]
        private MenuState InitialState = MenuState.Open;

        [SerializeField, FoldoutGroup("Settings")]
        private bool InstantOnEnable = true;
        
        [SerializeField, FoldoutGroup("Settings")]
        private float OpenTime;

        [SerializeField, FoldoutGroup("Settings")]
        private float CloseTime;

        [SerializeField, FoldoutGroup("Events")]
        private UnityEvent OnFinishOpen = new();

        [SerializeField, FoldoutGroup("Events")]
        private UnityEvent OnFinishClose = new();

        [SerializeField, FoldoutGroup("Dependencies"), ReadOnly]
        private CanvasGroup CanvasGroup;

        [SerializeField, FoldoutGroup("Dependencies"), ReadOnly]
        private Trigger MenuTrigger;

        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        public MenuState State;

        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private bool Initialized;

#if UNITY_EDITOR
        private void OnValidate() {
            if (TryGetComponent(out CanvasGroup cg)) {
                CanvasGroup = cg;
            }

            if (TryGetComponent(out Trigger trigger)) {
                MenuTrigger = trigger;
            }
        }
#endif
        private void OnEnable() {
            Initialized = false;
            if (!CanvasGroup) CanvasGroup = GetComponent<CanvasGroup>();
            if (!MenuTrigger) MenuTrigger = GetComponent<Trigger>();

            StartCoroutine(Init());

            IEnumerator Init() {
                if (MenuTrigger) {
                    while (!MenuTrigger.IsReady) {
                        yield return new WaitForEndOfFrame();
                    }
                }
                Initialized = true;
                if (InitialState == MenuState.Closed) {
                    Close(InstantOnEnable);
                }
                else {
                    Open(InstantOnEnable);
                }

                yield return new WaitForEndOfFrame();
            }
        }

        [Button]
        public void Toggle() {
            if (State == MenuState.Open) {
                Close();
            }
            else Open();
        }

        [Button]
        public void Open(bool instant = false) {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            MenuTrigger.SetState(true, instant);
            Activate();
        }

        [Button]
        public void Close(bool instant = false) {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            MenuTrigger.SetState(false, instant);
            Deactivate();
        }

        private void Activate() {
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.interactable = true;
            State = MenuState.Open;

            if (!Initialized) return;
            StartCoroutine(WaitToOpen());

            IEnumerator WaitToOpen() {
                yield return new WaitForSeconds(OpenTime);
                OnFinishOpen.Invoke();
            }
        }

        private void Deactivate() {
            CanvasGroup.blocksRaycasts = false;
            CanvasGroup.interactable = false;
            State = MenuState.Closed;

            if (!Initialized) return;
            StartCoroutine(WaitToClose());

            IEnumerator WaitToClose() {
                yield return new WaitForSeconds(CloseTime);
                OnFinishClose.Invoke();
            }
        }
    }
}