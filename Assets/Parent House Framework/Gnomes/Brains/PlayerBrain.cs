using System;
using UnityEngine.InputSystem;

namespace Parent_House_Framework.Gnomes.Brains {
    [Serializable]
    public class PlayerBrain: ActorBrain {

        public PlayerInput PossessingPlayer;

        public virtual void BindPlayerInput(PlayerInput playerInput) {
            PossessingPlayer = playerInput;
        }
    }
}