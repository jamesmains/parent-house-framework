using System;
using Parent_House_Framework.Gnomes.Behaviors;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Parent_House_Framework.Gnomes.Brains {
    [Serializable]
    public class PlayerBrain: ActorBrain {

        [SerializeField, FoldoutGroup("Status"),ReadOnly]
        public PlayerInput PossessingPlayer;

        [SerializeField, FoldoutGroup("Status"),ReadOnly]
        public Player Player;
        
        public virtual void BindPlayerInput(PlayerInput playerInput, Player player) {
            PossessingPlayer = playerInput;
            Player = player;
        }

        public override void Bind(Actor parent) {
            base.Bind(parent);
            // This return *should* only be used on instantiation if there is an object in the scene with a player brain.
            if (parent == null || Player == null) return;
        }
    }
}