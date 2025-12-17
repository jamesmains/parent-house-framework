using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Parent_House_Framework.Gnomes {
    /// <summary>
    /// This is the base brain class, it's used as the template for all brains to set
    /// values that the behaviors use to figure out what they need to be doing. These
    /// values are not all encompassing and will either need to be reworked to suit your needs
    /// OR I will need to figure out a better way to store this information so it's more malleable.
    /// </summary>
    [Serializable]
    public abstract class ActorBrain {
        [FoldoutGroup("Status"), ReadOnly] public Vector3 DesiredDirection; // Normalized vector for movement
        [FoldoutGroup("Status"), ReadOnly] public float DesiredMagnitude; // 0.0 to 1.0 (speed intent)
        [FoldoutGroup("Status"), ReadOnly] public Vector3 Velocity;

        [FoldoutGroup("Status"), ReadOnly]
        public Vector3 LookTarget; // World position to aim at (e.g., mouse or target AI position)
        [FoldoutGroup("Status"), ReadOnly] public Vector3 AngularInput;
        [FoldoutGroup("Status"), ReadOnly] public Vector3 AngularVelocity;
        
        [FoldoutGroup("Status"), ReadOnly] public bool Grounded;
        [FoldoutGroup("Status"), ReadOnly] public bool IsSprinting;
        
        [FoldoutGroup("Status"), ReadOnly] public bool JumpIntent;
        [FoldoutGroup("Status"), ReadOnly] public bool PrimaryActionIntent;
        [FoldoutGroup("Status"), ReadOnly] public bool SecondaryActionIntent;
        [FoldoutGroup("Status"), ReadOnly] public bool InteractActionIntent;

        [FoldoutGroup("Status"), ReadOnly] public Actor Parent;

        public virtual void Bind(Actor parent) {
            Parent = parent;
        }
        
        public virtual void Update(){}
        public virtual void FixedUpdate(){}
        public virtual void LateUpdate(){}
    }
}