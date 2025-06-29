using Sirenix.OdinInspector;
using UnityEngine;

namespace parent_house_framework.UI.Conditions {
    public class MenuStateCondition : Condition {
        [SerializeField, BoxGroup("Dependencies")]
        private Menu TargetMenu;

        public override bool IsConditionMet() {
            return TargetMenu.State == MenuState.Open;
        }
    }
}