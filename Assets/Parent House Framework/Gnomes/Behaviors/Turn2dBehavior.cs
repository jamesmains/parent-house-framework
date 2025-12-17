using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Parent_House_Framework.Gnomes.Behaviors {
    [Serializable]
    public class Turn2dBehavior: ActorBehavior {
        [Tooltip("The transform where you want the rotation to occur.")]
        [SerializeField, FoldoutGroup("Settings")]
        private Transform RootGfxTransform;

        [SerializeField, FoldoutGroup("Settings")]
        private float TurnSpeed = 12f;

        [SerializeField, FoldoutGroup("Settings")]
        private bool Invert = true;
        
        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private int FacingDirection = 1;

        public override void FixedUpdate() {
            base.FixedUpdate();
            var dir = Parent.Brain.DesiredDirection;
            int attemptedDirection = dir.x > 0 ? 1 : dir.x < 0 ? -1 : 0;
            attemptedDirection = Invert ?  -attemptedDirection : attemptedDirection;
            if (attemptedDirection != 0 && FacingDirection != attemptedDirection) {
                FacingDirection = attemptedDirection;
            }
            var scale = RootGfxTransform.localScale;
            //if (Mathf.Approximately(scale.x, FacingDirection)) return;
            scale.x = Mathf.MoveTowards(scale.x, FacingDirection, TurnSpeed * Time.deltaTime);
            RootGfxTransform.localScale = scale;
        }
    }
}