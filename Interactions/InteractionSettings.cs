using System;

namespace parent_house_framework.Interactions {
    [Serializable]
    public class InteractionSettings
    {
        public InteractionSettings() {
        }

        public bool RequireKeyToActivate;
        public bool ShowInteractPrompt;
        public bool ActiveOnEnable;
        public bool InstantOnEnable;
        public bool ReadyOnEnable;
        public bool Toggles;
        
        // Todo: Work in progress
        public bool AlwaysTriggers;
        public bool ResetOnceFinished;
    }
}