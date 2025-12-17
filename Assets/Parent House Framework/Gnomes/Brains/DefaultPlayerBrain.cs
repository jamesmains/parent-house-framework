using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Parent_House_Framework.Gnomes.Brains {
    [Serializable]
    public class DefaultPlayerBrain : PlayerBrain {
        //Use your action's exact name, Input Action Reference did not function when the Actor was turned into a prefab.

        [Tooltip("The EXACT string name of the Movement Action in the Input Asset.")]
        [SerializeField, FoldoutGroup("Settings")]
        private string MovementActionName = "Move";

        [Tooltip("The EXACT string name of the Jump Action in the Input Asset.")]
        [SerializeField, FoldoutGroup("Settings")]
        private string JumpActionName = "Jump";

        [Tooltip("The EXACT string name of the Interact Action in the Input Asset.")]
        [SerializeField, FoldoutGroup("Settings")]
        private string InteractActionName = "Interact";

        [Tooltip("The EXACT string name of the Primary Action in the Input Asset.")]
        [SerializeField, FoldoutGroup("Settings")]
        private string PrimaryActionName = "PrimaryAction";

        [Tooltip("The EXACT string name of the Secondary Action in the Input Asset.")]
        [SerializeField, FoldoutGroup("Settings")]
        private string SecondaryActionName = "SecondaryAction";

        // Private fields to hold the actions found at runtime
        private InputAction _movementAction;
        private InputAction _jumpAction;
        private InputAction _interactAction;
        private InputAction _primaryAction;
        private InputAction _secondaryAction;

        public override void BindPlayerInput(PlayerInput possessingPlayer) {
            // --- SETUP (Find and Subscribe on NEW player) ---
            base.BindPlayerInput(possessingPlayer);
            if (PossessingPlayer == null) return;

            // Find the action by string name on the possessed PlayerInput's current map
            PossessingPlayer.currentActionMap?.Enable();
            BindMovement();
            BindJump();
            BindInteract();
            BindPrimaryAction();
            BindSecondaryAction();
        }

        // --- CLEANUP (Unsubscribe from OLD player) ---
        private void UnbindAll() {
            if (_movementAction != null) {
                _movementAction.performed -= HandleMovement;
                _movementAction.canceled -= HandleMovement;
                _movementAction = null; // Clear the reference
            }

            if (_jumpAction != null) {
                _jumpAction.performed -= HandleJump;
                _jumpAction = null;
            }

            if (_interactAction != null) {
                _interactAction.performed -= HandleInteract;
                _interactAction = null;
            }

            if (_primaryAction != null) {
                _primaryAction.performed -= HandlePrimaryAction;
                _primaryAction = null;
            }

            if (_secondaryAction != null) {
                _secondaryAction.performed -= HandleSecondaryAction;
                _secondaryAction = null;
            }
        }

        private void BindMovement() {
            _movementAction = PossessingPlayer.currentActionMap?.FindAction(MovementActionName);

            if (_movementAction != null) {
                // Subscribe to the reliable action events
                _movementAction.performed += HandleMovement;
                _movementAction.canceled += HandleMovement;

                // Ensure the action map and the action itself are enabled
                _movementAction.Enable();

                Debug.Log(
                    $"P{PossessingPlayer.playerIndex}: Successfully bound '{MovementActionName}' using string lookup.");
            }
            else {
                Debug.LogError(
                    $"Action '{MovementActionName}' not found in map '{PossessingPlayer.currentActionMap?.name}'. Check the spelling and the PlayerInput component setup.");
            }
        }

        private void BindJump() {
            _jumpAction = PossessingPlayer.currentActionMap?.FindAction(JumpActionName);
            if (_jumpAction != null) {
                _jumpAction.performed += HandleJump;
                _jumpAction.canceled += HandleJump;
                _jumpAction.Enable();
                Debug.Log(
                    $"P{PossessingPlayer.playerIndex}: Successfully bound '{JumpActionName}' using string lookup.");
            }
            else {
                Debug.LogError(
                    $"Action '{JumpActionName}' not found in map '{PossessingPlayer.currentActionMap?.name}'. Check the spelling and the PlayerInput component setup.");
            }
        }

        private void BindInteract() {
            _interactAction = PossessingPlayer.currentActionMap?.FindAction(InteractActionName);
            if (_interactAction != null) {
                _interactAction.performed += HandleInteract;
                _interactAction.Enable();
                Debug.Log(
                    $"P{PossessingPlayer.playerIndex}: Successfully bound '{InteractActionName}' using string lookup.");
            }
            else {
                Debug.LogError(
                    $"Action '{InteractActionName}' not found in map '{PossessingPlayer.currentActionMap?.name}'. Check the spelling and the PlayerInput component setup.");
            }
        }

        private void BindPrimaryAction() {
            _primaryAction = PossessingPlayer.currentActionMap?.FindAction(PrimaryActionName);
            if (_primaryAction != null) {
                _primaryAction.performed += HandlePrimaryAction;
                _primaryAction.Enable();
                Debug.Log(
                    $"P{PossessingPlayer.playerIndex}: Successfully bound '{PrimaryActionName}' using string lookup.");
            }
            else {
                Debug.LogError(
                    $"Action '{PrimaryActionName}' not found in map '{PossessingPlayer.currentActionMap?.name}'. Check the spelling and the PlayerInput component setup.");
            }
        }

        private void BindSecondaryAction() {
            _secondaryAction = PossessingPlayer.currentActionMap?.FindAction(SecondaryActionName);
            if (_secondaryAction != null) {
                _secondaryAction.performed += HandleSecondaryAction;
                _secondaryAction.Enable();
                Debug.Log(
                    $"P{PossessingPlayer.playerIndex}: Successfully bound '{SecondaryActionName}' using string lookup.");
            }
            else {
                Debug.LogError(
                    $"Action '{SecondaryActionName}' not found in map '{PossessingPlayer.currentActionMap?.name}'. Check the spelling and the PlayerInput component setup.");
            }
        }

        /// <summary>
        /// Read the input from the player's device and either set the DesiredDirection or reset it based on the state.
        /// </summary>
        /// <param name="ctx"></param>
        private void HandleMovement(InputAction.CallbackContext ctx) {
            // This function only runs for the correct action, as it's directly subscribed to _movementAction
            if (ctx.performed) {
                DesiredDirection = ctx.ReadValue<Vector2>();
                // This input is guaranteed to be from the device assigned to this PossessingPlayer
                // ... apply movement logic ...
            }
            else if (ctx.canceled) {
                // ... reset movement logic ...
                DesiredDirection = ctx.ReadValue<Vector2>();
            }
        }

        /// <summary>
        /// This sets the intent to jump, it's up to the behavior that consumes it if it's valid or not.
        /// </summary>
        /// <param name="callbackContext"></param>
        private void HandleJump(InputAction.CallbackContext callbackContext) {
            JumpIntent = callbackContext.phase == InputActionPhase.Started;
        }

        /// <summary>
        /// Simple action intent, it can only be true since we only listen for when it's performed.
        /// </summary>
        /// <param name="callbackContext"></param>
        private void HandleInteract(InputAction.CallbackContext callbackContext) {
            InteractActionIntent = true;
        }

        /// <summary>
        /// Simple action intent, it can only be true since we only listen for when it's performed.
        /// </summary>
        /// <param name="callbackContext"></param>
        private void HandlePrimaryAction(InputAction.CallbackContext callbackContext) {
            PrimaryActionIntent = true;
        }

        /// <summary>
        /// Simple action intent, it can only be true since we only listen for when it's performed.
        /// </summary>
        /// <param name="callbackContext"></param>
        private void HandleSecondaryAction(InputAction.CallbackContext callbackContext) {
            SecondaryActionIntent = true;
        }
    }
}