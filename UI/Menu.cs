using System.Collections;
using parent_house_framework.Interactions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace parent_house_framework.UI {
    public enum MenuState {
        Closed,
        Open
    }

    [RequireComponent(typeof(CanvasGroup))]
    public class Menu : SerializedMonoBehaviour {
        [SerializeField]
        private MenuSettings Settings;        

        [SerializeField] [FoldoutGroup("Dependencies")] [ReadOnly]
        private CanvasGroup CanvasGroup;

        [SerializeField] [FoldoutGroup("Dependencies")] [ReadOnly]
        private Trigger MenuTrigger;

        [SerializeField] [FoldoutGroup("Status")] [ReadOnly]
        public MenuState State;

        [SerializeField] [FoldoutGroup("Status")] [ReadOnly]
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
            CanvasGroup ??= GetComponent<CanvasGroup>();
            MenuTrigger ??= GetComponent<Trigger>();

            if (Settings.InitialState == MenuState.Closed) {
                Open(true);
                Initialized = true;
                Close(Settings.InstantOnEnable);
            }
            else {
                Close(true);
                Initialized = true;
                Open(Settings.InstantOnEnable);
            }

            StartCoroutine(Init());

            IEnumerator Init() {
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
            MenuTrigger?.SetState(true);
            Activate();
        }

        [Button]
        public void Close(bool instant = false) {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            MenuTrigger?.SetState(false);
            Deactivate();
        }

        private void Activate() {
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.interactable = true;
            State = MenuState.Open;

            if (!Initialized) return;
            
            Settings.OnStartOpen.Invoke();
            
            StartCoroutine(WaitToOpen());

            IEnumerator WaitToOpen() {
                yield return new WaitForSeconds(Settings.OpenTime);
                Settings.OnFinishOpen.Invoke();
            }
        }

        private void Deactivate() {
            CanvasGroup.blocksRaycasts = false;
            CanvasGroup.interactable = false;
            State = MenuState.Closed;

            if (!Initialized) return;
            
            Settings.OnStartClose.Invoke();
            
            StartCoroutine(WaitToClose());

            IEnumerator WaitToClose() {
                yield return new WaitForSeconds(Settings.CloseTime);
                Settings.OnFinishClose.Invoke();
            }
        }
    }
}