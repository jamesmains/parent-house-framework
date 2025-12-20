using System.Collections.Generic;
using Parent_House_Framework.Core;
using Parent_House_Framework.Gnomes.Brains;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Parent_House_Framework.Gnomes.Demo_3d {
    public class DemoController3d: MonoBehaviour {
        [SerializeField,FoldoutGroup("Dependencies")]
        private PlayerInputManager InputManager;
        
        [SerializeField,FoldoutGroup("Settings")]
        private GameObject PlayerActorPrefab;

        private void OnEnable() {
            Debug.Log("Binding onPlayerJoined to SpawnPlayer");
            InputManager.onPlayerJoined += SpawnPlayer;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable() {
            InputManager.onPlayerJoined -= SpawnPlayer;
        }

        private void SpawnPlayer(PlayerInput playerInput) {
            Debug.Log("Spawning Player");
            var spawnedActor = Pooler.Spawn(PlayerActorPrefab).GetComponent<Actor>();
            var newBrain = new DefaultPlayerBrain();
            newBrain.BindPlayerInput(playerInput,playerInput.gameObject.GetComponent<Player>());
            spawnedActor.SetPossession(newBrain);
        }
    }
}