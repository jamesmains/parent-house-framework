using System;
using parent_house_framework.Conditions;

namespace parent_house_framework.UI.Modal {
    [Serializable]
    public class ModalOpenCondition: Condition {
        public override bool IsConditionMet() {
            return ModalManager.IsModalOpen;
        }
    }
}