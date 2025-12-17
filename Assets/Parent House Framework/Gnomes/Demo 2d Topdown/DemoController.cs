using System.Collections.Generic;
using Parent_House_Framework.Core;
using Parent_House_Framework.Gnomes.Brains;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Parent_House_Framework.Gnomes.Demo_2d_Topdown {
    public class DemoController: MonoBehaviour {
        [SerializeField,FoldoutGroup("Dependencies")]
        private PlayerInputManager InputManager;
        
        [SerializeField,FoldoutGroup("Settings")]
        private GameObject PlayerActorPrefab;
        
        [SerializeField,FoldoutGroup("Settings")]
        private List<GameObject> RandomActors;

        private void OnEnable() {
            Debug.Log("Binding onPlayerJoined to SpawnPlayer");
            InputManager.onPlayerJoined += SpawnPlayer;
        }

        private void OnDisable() {
            InputManager.onPlayerJoined -= SpawnPlayer;
        }

        private void SpawnPlayer(PlayerInput playerInput) {
            Debug.Log("Spawning Player");
            var spawnedActor = Pooler.Spawn(PlayerActorPrefab).GetComponent<Actor>();
            var newBrain = new DefaultPlayerBrain();
            newBrain.BindPlayerInput(playerInput);
            spawnedActor.SetPossession(newBrain);
        }

        [Button]
        private void SpawnRandomActor() {
            Pooler.Spawn(RandomActors[UnityEngine.Random.Range(0, RandomActors.Count)]);
        }
    }
}