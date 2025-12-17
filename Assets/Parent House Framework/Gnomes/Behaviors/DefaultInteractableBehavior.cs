using System;
using Parent_House_Framework.Core;
using Parent_House_Framework.Gnomes.Brains;
using Parent_House_Framework.Interactions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Parent_House_Framework.Gnomes.Behaviors {
    [Serializable]
    public class DefaultInteractableBehavior: ActorBehavior, IInteract {
        public void Interact(Actor interactingActor) {
            if (Parent?.Brain != null) Parent.Brain.PrimaryActionIntent = true;
        }
    }

    [Serializable]
    public class MountHorseInteractableBehavior : ActorBehavior, IInteract {
        [SerializeField, FoldoutGroup("Settings")]
        private GameObject HorseMountActor;
        public void Interact(Actor interactingActor) {
            if (interactingActor?.Brain is not PlayerBrain brain || HorseMountActor == null) return;
            var horseMount = Pooler.SpawnAt(HorseMountActor, interactingActor.transform.position);
            if (!horseMount.TryGetComponent<Actor>(out var horseMountActor)) {
                horseMount.SetActive(false);
                return;
            }

            horseMountActor.SetPossession(brain);
            interactingActor.gameObject.SetActive(false);
            Parent.gameObject.SetActive(false);
        }
    }
}