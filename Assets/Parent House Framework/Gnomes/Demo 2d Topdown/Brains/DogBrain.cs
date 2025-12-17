using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Parent_House_Framework.Gnomes.Demo_2d_Topdown.Brains {
    /// <summary>
    /// This is an example brain that simply tries to find a direction it
    /// wants to go in and sets the DesiredDirection to it.
    /// </summary>
    [Serializable]
    public class DogBrain: ActorBrain {
        
        [SerializeField, FoldoutGroup("Settings")]
        private Vector2 SeekTimeRange;
        
        [SerializeField, FoldoutGroup("Status"),ReadOnly]
        private float NextSeekTime;
        
        [SerializeField, FoldoutGroup("Status"),ReadOnly]
        private float LastSeekTime;

        public override void Update() {
            base.Update();
            if (Time.time >= LastSeekTime + NextSeekTime) {
                LastSeekTime = Time.time;
                NextSeekTime = Time.time + Random.Range(SeekTimeRange.x, SeekTimeRange.y);
                DesiredDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
        }
    }
}