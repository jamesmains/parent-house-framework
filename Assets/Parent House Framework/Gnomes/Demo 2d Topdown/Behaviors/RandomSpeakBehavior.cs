using System;
using System.Collections.Generic;
using Parent_House_Framework.Gnomes.Brains;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Parent_House_Framework.Gnomes.Demo_2d_Topdown.Behaviors {
    /// <summary>
    /// There are parts of this that could be reasonably attached to the ActorBrain since
    /// this behavior is making decisions AND executing them, but this is to show that
    /// there could be cases where you want to mix them. There are no set rules.
    ///
    /// The core portions that would NOT be on the ActorBrain are DialogueBox, DialogueText,
    /// TextOptions, and the portion in Update() above the CanBarkAutomatically check that
    /// dictates when it will bark again. CanBarkAutomatically would probably not be needed
    /// if this logic was on the ActorBrain since the only case this is false is when the Player
    /// possesses the parent Actor.
    /// </summary>
    [Serializable]
    public class RandomSpeakBehavior: ActorBehavior {
        [SerializeField, FoldoutGroup("Dependencies")]
        private GameObject DialogueBox;
        
        [SerializeField, FoldoutGroup("Dependencies")]
        private TextMeshProUGUI DialogueText;
        
        [SerializeField, FoldoutGroup("Settings")]
        private Vector2 SpeakTimeRange;
        
        [SerializeField, FoldoutGroup("Settings")]
        private List<string> TextOptions = new();
        
        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private float NextSpeakTime;
        
        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private float LastSpeakTime;

        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private bool CanSpeakAutomatically;

        public override void Bind(Actor parentActor) {
            base.Bind(parentActor);
            DialogueBox.SetActive(false);
            NextSpeakTime = Time.time + Random.Range(SpeakTimeRange.x, SpeakTimeRange.y);
            CanSpeakAutomatically = Parent.Brain is not PlayerBrain;
        }

        public override void Update() {
            base.Update();

            if (Parent.Brain.PrimaryActionIntent) {
                DoSpeak();
            }
            
            if (Time.time >= LastSpeakTime + 2f) {
                DialogueBox.SetActive(false);
            }
            
            if (!CanSpeakAutomatically) return;
            if (Time.time >= LastSpeakTime + NextSpeakTime) {
                NextSpeakTime = Time.time + Random.Range(SpeakTimeRange.x, SpeakTimeRange.y);
                Parent.Brain.PrimaryActionIntent = true;
            }
        }

        public override void HandlePossessionChange(bool isPossessed) {
            base.HandlePossessionChange(isPossessed);
            CanSpeakAutomatically = !isPossessed;
        }

        private void DoSpeak() {
            LastSpeakTime = Time.time;
            Parent.Brain.PrimaryActionIntent = false;
            DialogueBox.SetActive(true);
            DialogueText.text = TextOptions[Random.Range(0, TextOptions.Count)];
        }
    }
}