using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Parent_House_Framework.Gnomes.Behaviors {
    [Serializable]
    public abstract class CameraBehavior: ActorBehavior {
        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        public Camera Cam;

        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        public Actor Target;
    }
}