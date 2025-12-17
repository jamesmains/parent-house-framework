using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Parent_House_Framework.Gnomes.Behaviors {
    [Serializable]
    public class AnimateBehavior: ActorBehavior {
        private static readonly int IsMoving = Animator.StringToHash("isMoving");

        [SerializeField, FoldoutGroup("Dependencies")]
        private Animator ActorAnimator;

        public override void FixedUpdate() {
            base.FixedUpdate();
            ActorAnimator.SetBool(IsMoving,Parent.Brain.DesiredMagnitude != 0);
        }
        
        public override void Bind(Actor parentActor) {
            base.Bind(parentActor);
            if(ActorAnimator == null)
                ActorAnimator = parentActor.GetComponentInChildren<Animator>();
        }
    }
}