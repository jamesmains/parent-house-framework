using System;
using Parent_House_Framework.Interactions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Parent_House_Framework.Gnomes.Behaviors {
    [Serializable]
    public class InteractorBehavior: ActorBehavior {
        [SerializeField, FoldoutGroup("Dependencies")]
        private Interactor Interactor;

        public override void Update() {
            base.Update();
            if (!Parent.Brain.PrimaryActionIntent) return;
            var nearestInteractable = Interactor.GetNearestInteractable(Parent.transform);
            nearestInteractable?.Interact(Parent);
            Parent.Brain.PrimaryActionIntent = false;
        }
    }
}