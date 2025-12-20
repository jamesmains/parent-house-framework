using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Parent_House_Framework.Gnomes.Behaviors {
    [Serializable]
    public class Movement3dBehavior: ActorBehavior {
        [SerializeField, FoldoutGroup("Dependencies")]
        private Rigidbody Rigidbody;

        [SerializeField, FoldoutGroup("Settings")]
        public float BaseSpeed = 20f; // Increased for AddForce

        [SerializeField, FoldoutGroup("Settings")]
        public float MaxVelocity = 12f;

        [SerializeField, FoldoutGroup("Settings")]
        public float TurnSpeed = 100f;

        public override void FixedUpdate() {
            base.FixedUpdate();
            if (Rigidbody == null || Parent.Brain == null) return;

            HandleRotation();
            HandleMovement();
        }

        private void HandleRotation() {
            // Get input (usually Mouse X and Y)
            Vector2 lookInput = Parent.Brain.AngularInput;

            // 1. Calculate the rotation change as a Quaternion instead of Euler Angles
            // This prevents the "weird flips" at 0/360 degrees
            Quaternion yRotation = Quaternion.AngleAxis(lookInput.x * TurnSpeed * Time.fixedDeltaTime, Vector3.up);
            Quaternion xRotation = Quaternion.AngleAxis(-lookInput.y * TurnSpeed * Time.fixedDeltaTime, Vector3.right);

            // 2. Apply rotation to the Rigidbody or Transform
            // Note: In 3D characters, we usually rotate the Body on Y and the Camera on X.
            // For now, we apply Y to the whole Parent.
            Rigidbody.MoveRotation(Rigidbody.rotation * yRotation);
        }

        private void HandleMovement() {
            Vector2 moveInput = Parent.Brain.DesiredDirection;
            Parent.Brain.DesiredMagnitude = moveInput.sqrMagnitude > 0 ? 1 : 0;

            // 1. Calculate direction based on where the Parent is CURRENTLY facing
            Vector3 moveDirection = (Parent.transform.forward * moveInput.y) + (Parent.transform.right * moveInput.x);

            // 2. Limit Velocity
            // If we are already at MaxVelocity, don't add more force in that direction
            if (Rigidbody.linearVelocity.magnitude < MaxVelocity) {
                Rigidbody.AddForce(moveDirection * BaseSpeed, ForceMode.Acceleration);
            }

            // 3. Friction/Braking
            // If there's no input, help the character stop so they don't slide forever
            if (moveInput.sqrMagnitude < 0.01f) {
                Vector3 brakeForce = -Rigidbody.linearVelocity;
                brakeForce.y = 0; // Don't stop gravity
                Rigidbody.AddForce(brakeForce * (BaseSpeed * 0.5f), ForceMode.Acceleration);
            }
        }
    }
}