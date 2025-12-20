using System;
using System.Linq;
using Parent_House_Framework.Core;
using Parent_House_Framework.Gnomes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Parent_House_Framework.Interactions {
    [Serializable]
    public class InteractionsDictionary : UnitySerializedDictionary<IInteract, Actor> {
    }

    public enum InteractionCollisionType {
        Collider,
        Collider2d
    }

    public class Interactor : MonoBehaviour {
        [SerializeField, FoldoutGroup("Settings")]
        public InteractionCollisionType CollisionType;

        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private InteractionsDictionary InteractableTargets;

        public IInteract GetNearestInteractable(Transform referencePoint) {
            if (InteractableTargets.Count <= 0) {
                return null;
            }
            return InteractableTargets.OrderBy(o=>Vector3.Distance(o.Value.transform.position,referencePoint.position)).First().Key;
        }
        
        private void OnTriggerEnter(Collider other) {
            if (CollisionType != InteractionCollisionType.Collider) return;
            if (!other.TryGetComponent<Actor>(out var actor) || actor.GetBehavior<IInteract>(out var interact) == null) return;
            InteractableTargets.TryAdd(interact, actor);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (CollisionType != InteractionCollisionType.Collider2d) return;
            if (!other.TryGetComponent<Actor>(out var actor) || actor.GetBehavior<IInteract>(out var interact) == null) return;
            InteractableTargets.TryAdd(interact, actor);
        }

        private void OnTriggerExit(Collider other) {
            if (CollisionType != InteractionCollisionType.Collider) return;
            if (!other.TryGetComponent<Actor>(out var actor) || actor.GetBehavior<IInteract>(out var interact) == null) return;
            InteractableTargets.Remove(interact);
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (CollisionType != InteractionCollisionType.Collider2d) return;
            if (!other.TryGetComponent<Actor>(out var actor) || actor.GetBehavior<IInteract>(out var interact) == null) return;
            InteractableTargets.Remove(interact);
        }
    }
}