using System;
using UnityEngine;

namespace Parent_House_Framework.Gnomes.Behaviors {
    /// <summary>
    /// Basic 2d Movement based on ActorBrain's DesiredDirection
    /// Todo:
    /// [] Implement Velocity and Drag.
    /// </summary>
    [Serializable]
    public class Movement2dBehavior : ActorBehavior {
        public float BaseSpeed = 2;
        public float MaxVelocity = 12;
        public override void FixedUpdate() {
            base.FixedUpdate();
            Parent.Brain.DesiredMagnitude = Parent.Brain.DesiredDirection == Vector3.zero ? 0 : 1;
            var pos = Parent.transform.position;
            pos.x += Parent.Brain.DesiredDirection.x * BaseSpeed * Time.deltaTime;
            pos.y += Parent.Brain.DesiredDirection.y * BaseSpeed * Time.deltaTime;
            Parent.transform.position = pos;
        }
    }
}
