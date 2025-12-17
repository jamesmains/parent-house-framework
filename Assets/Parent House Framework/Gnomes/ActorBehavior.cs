using System;
using UnityEngine;

namespace Parent_House_Framework.Gnomes {
    [Serializable]
    public abstract class ActorBehavior {
        [SerializeField]
        protected Actor Parent;
        public virtual void Bind(Actor parentActor) {
            Parent =  parentActor;
            Debug.Log("Bound to " + Parent.gameObject.name);
        }

        public virtual void Awake() {
            
        }

        public virtual void Update() {
        
        }
    
        public virtual void FixedUpdate() {
        
        }
        
        public virtual void HandlePossessionChange(bool isPossessed) {}
        
    }
}
