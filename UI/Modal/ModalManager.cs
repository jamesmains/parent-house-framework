using System;
using System.Collections.Generic;
using System.Linq;
using parent_house_framework.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace parent_house_framework.UI.Modal {
    public enum ModalTypes {
        DefaultBox,
    }

    public class ModalManager : MonoBehaviour {
        
        /// <summary>
        /// Todo:
        /// May need a queue if multiple modal menus are required. Currently I don't see that being a thing.
        /// If it is turned into a queue, the static bool may need to be converted to an int counter
        /// </summary>
        
        [SerializeField, FoldoutGroup("Settings")]
        private List<ModalOption> ModalOptions = new();

        public static Action<Modal> OnSendModal;
        public static bool IsModalOpen;

        private void OnEnable() {
            OnSendModal += HandleModal;
        }

        private void OnDisable() {
            OnSendModal -= HandleModal;
        }

        private void HandleModal(Modal modal) {
            var option = ModalOptions.FirstOrDefault(o => o.Type == modal.Type);
            if (option == null) {
                UnityEngine.Debug.LogError($"Modal type {modal.Type} not found");
                return;
            }
            
            var modalObj = Pooler.Spawn(option.Prefab, option.Container);
            if (modalObj.TryGetComponent(out ModalDisplay modalDisplay)) {
                modalDisplay.Build(modal);
            }
        }
        
        [Button]
        private void DebugSendDefaultModal(string msg) {
            Modal modal = new Modal(msg, null,null,ModalTypes.DefaultBox);
            modal.Send();
        }
    }

    [Serializable]
    public class ModalOption {
        public ModalTypes Type;
        public GameObject Prefab;
        public RectTransform Container;
    }

    public class Modal {
        public Modal(string msg, Action confirmAction, Action cancelAction, ModalTypes type = ModalTypes.DefaultBox) {
            Message = msg;
            ConfirmAction = confirmAction;
            CancelAction = cancelAction;
            Type = type;
        }
        public ModalTypes Type;
        public string Message;
        public Action ConfirmAction;
        public Action CancelAction;

        public void Send() {
            ModalManager.OnSendModal?.Invoke(this);
        }
    }
}