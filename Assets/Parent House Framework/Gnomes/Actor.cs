using System.Collections.Generic;
using Parent_House_Framework.Gnomes.Behaviors;
using Parent_House_Framework.Gnomes.Brains;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Parent_House_Framework.Gnomes {
    public class Actor : SerializedMonoBehaviour {
        [SerializeField, FoldoutGroup("Settings")]
        private ActorBrain DefaultBrain;

        [SerializeField, FoldoutGroup("Settings")]
        public CameraBehavior DefaultCameraBehavior;

        [SerializeField, FoldoutGroup("Settings")]
        public List<ActorBehavior> InitialBehaviors = new();

        [SerializeField, FoldoutGroup("Settings")]
        public Transform CameraRoot;

        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private List<ActorBehavior> ActiveBehaviors = new();

        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        public ActorBrain Brain;

        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        public CameraBehavior CameraBehavior;

        private void Awake() {
            DefaultBrain ??= new DummyBrain();
            Brain = DefaultBrain;
            Brain?.Bind(this);

            // Determine default camera and set to null
            DefaultCameraBehavior ??= new DefaultCameraBehavior();
            CameraBehavior = null;
            
            foreach (ActorBehavior behavior in InitialBehaviors) {
                ActiveBehaviors.Add(behavior);
                behavior.Bind(this);
            }
        }

        public void SetPossession(PlayerBrain playerBrain) {
            playerBrain?.Parent?.SetPossession(null);
            Brain = playerBrain ?? DefaultBrain;
            ActiveBehaviors.ForEach(o => o.HandlePossessionChange(playerBrain != null));
            Brain.Bind(this);

            if (playerBrain != null) {
                CameraBehavior = playerBrain.OverrideCameraBehavior ?? DefaultCameraBehavior;
                CameraBehavior?.Bind(this);
                if (CameraBehavior != null) {
                    CameraBehavior.Cam = playerBrain.Player.Cam;
                    CameraBehavior.Target = this;
                }
            }
            else CameraBehavior = null;
        }

        public void AddBehavior(ActorBehavior behavior) {
            ActiveBehaviors.Add(behavior);
            behavior.Bind(this);
        }

        public void RemoveBehavior(ActorBehavior behavior) {
            ActiveBehaviors.Remove(behavior);
        }

        public T GetBehavior<T>(out T component) where T : class {
            return component = ActiveBehaviors.Find(o => o is T) as T;
        }

        private void Update() {
            ActiveBehaviors.ForEach(o => o.Update());
            Brain?.Update();
            CameraBehavior?.Update();
        }

        private void FixedUpdate() {
            ActiveBehaviors.ForEach(o => o.FixedUpdate());
            Brain?.FixedUpdate();
            CameraBehavior?.FixedUpdate();
        }
    }
}