using System;
using Parent_House_Framework.Interactions;

namespace Parent_House_Framework.Gnomes.Behaviors {
    [Serializable]
    public class DefaultInteractableBehavior: ActorBehavior, IInteract {
        public void Interact(Actor interactingActor) {
            if (Parent?.Brain != null) Parent.Brain.PrimaryActionIntent = true;
        }
    }
}