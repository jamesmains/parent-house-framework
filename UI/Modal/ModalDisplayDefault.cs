using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace parent_house_framework.UI.Modal {
    public class ModalDisplayDefault : ModalDisplay {
        [SerializeField, FoldoutGroup("Dependencies")]
        private Menu ModalMenu;

        [SerializeField, FoldoutGroup("Dependencies")]
        private Button ConfirmButton;

        [SerializeField, FoldoutGroup("Dependencies")]
        private Button CancelButton;

        public override void Build(Modal modal) {
            base.Build(modal);

            ModalText.text = modal.Message;
            
            ConfirmButton.onClick.RemoveAllListeners();
            CancelButton.onClick.RemoveAllListeners();

            ConfirmButton.onClick.AddListener(delegate { HandleButtonClick(modal.ConfirmAction); });
            CancelButton.onClick.AddListener(delegate { HandleButtonClick(modal.CancelAction); });

            ModalManager.IsModalOpen = true;
            
            ModalMenu.Open();
        }

        private void HandleButtonClick(Action action) {
            action?.Invoke();
            ModalMenu.Close();
            ModalManager.IsModalOpen = false;
        }
    }
}