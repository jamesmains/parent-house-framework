using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Parent_House_Framework.Gnomes.Behaviors {
    [Serializable]
    public class DefaultCameraBehavior: CameraBehavior {
        [SerializeField, FoldoutGroup("Settings")] 
        private Vector3 Offset;

        [SerializeField, FoldoutGroup("Settings")]
        private float Distance;

        [SerializeField, FoldoutGroup("Settings")]
        private float RotateSpeed = 20;

        [SerializeField, FoldoutGroup("Settings")]
        private float FollowSpeed = 50;
        
        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private Vector3 TargetPosition;
        
        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private Vector3 TargetRotation;
        
        public float SmoothTime = 0.15f; // Time to reach target
        private Vector3 _currentVelocity = Vector3.zero; // Needed for SmoothDamp
        
        public override void FixedUpdate() {

            if (!Target) return;

            // 1. Determine the anchor point
            Transform cameraRoot = Target.CameraRoot ? Target.CameraRoot : Target.transform;
    
            // 2. Calculate the Ideal Position
            // We calculate the position based on the Target's current forward vector
            Vector3 targetPos = cameraRoot.position + Offset + (-Target.transform.forward * Distance);

            // 3. Smooth Movement (SmoothDamp is superior to Lerp for cameras)
            Cam.transform.position = Vector3.SmoothDamp(
                Cam.transform.position, 
                targetPos, 
                ref _currentVelocity, 
                SmoothTime
            );

            // 4. Smooth Rotation (Using Slerp to prevent flipping)
            // We look at the Root, but you can also just match Target.transform.rotation
            //Quaternion targetRot = Quaternion.LookRotation(cameraRoot.position + Offset - Cam.transform.position);
    
            Cam.transform.rotation = Quaternion.Slerp(
                Cam.transform.rotation, 
                cameraRoot.rotation, 
                RotateSpeed * Time.fixedDeltaTime
            );
            
        }
    }
}