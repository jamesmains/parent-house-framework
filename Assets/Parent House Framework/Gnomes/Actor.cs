using System.Collections.Generic;
using Parent_House_Framework.Gnomes.Brains;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Parent_House_Framework.Gnomes {
    public class Actor : SerializedMonoBehaviour {

        [SerializeField, FoldoutGroup("Settings")]
        private ActorBrain DefaultBrain;
        
        [SerializeField, FoldoutGroup("Settings")]
        public List<ActorBehavior> InitialBehaviors = new();
        
        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private List<ActorBehavior> ActiveBehaviors = new();
        
        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        public ActorBrain Brain;

        private void Awake() {
            DefaultBrain ??= new DummyBrain();
            Brain = DefaultBrain;
            Brain?.Bind(this);
            foreach (ActorBehavior behavior in InitialBehaviors) {
                ActiveBehaviors.Add(behavior);
                behavior.Bind(this);
            }
        }

        [Button]
        public void SetPossession(PlayerBrain playerBrain) {
            Brain = playerBrain ?? DefaultBrain;
            ActiveBehaviors.ForEach(o=>o.HandlePossessionChange(playerBrain != null));
            // Debug camera
            if (playerBrain != null) {
                playerBrain.Parent?.SetPossession(null);
                playerBrain.Bind(this);
                playerBrain.PossessingPlayer.transform.SetParent(transform);
                var pos  = transform.position;
                pos.z -= 14f;
                playerBrain.PossessingPlayer.transform.position = pos;
            }
        }

        public void AddBehavior(ActorBehavior behavior) {
            ActiveBehaviors.Add(behavior);
            behavior.Bind(this);
        }

        public void RemoveBehavior(ActorBehavior behavior) {
            ActiveBehaviors.Remove(behavior);
        }

        public T GetBehavior<T>() where T : class {
            return ActiveBehaviors.Find(o=>o is T) as T;
        }

        private void Update() {
            ActiveBehaviors.ForEach(o=>o.Update());
            Brain?.Update();
        }

        private void FixedUpdate() {
            ActiveBehaviors.ForEach(o=>o.FixedUpdate());
            Brain?.FixedUpdate();
        }
    }
}
