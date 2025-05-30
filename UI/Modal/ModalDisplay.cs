using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace parent_house_framework.UI.Modal {
    public abstract class ModalDisplay : MonoBehaviour {
        [SerializeField, FoldoutGroup("Dependencies")]
        protected TextMeshProUGUI ModalText;
        public virtual void Build(Modal modal) {
        }
    }
}