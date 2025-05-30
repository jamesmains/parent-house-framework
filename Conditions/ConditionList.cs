using Sirenix.OdinInspector;
using UnityEngine;

namespace parent_house_framework.Conditions {
    /// <summary>
    /// The point of this is so that there is a global list of conditions for something without needing
    /// to set the conditions on an object every time or if the conditions are lost.
    /// I.e. an Actor in GNOMES all use the same list but something happens to the reference, just the root actor
    /// prefab would need a re-reference to this global list.
    /// </summary>
    [CreateAssetMenu(fileName = "Condition List", menuName = "Conditions/Condition List")]
    public class ConditionList: SerializedScriptableObject {
        [SerializeField]
        private Condition Conditions;
        public bool ConditionsMet() {
            if(Conditions == null ) return true;
            else return Conditions.IsConditionMet();
        }
    }
}