using System;

namespace Parent_House_Framework.Interactions {
    [Serializable]
    public class InteractionSettings
    {
        public InteractionSettings() {
        }

        public bool RequireKeyToActivate;
        public bool ShowInteractPrompt;
        public bool ActiveOnEnable;
        public bool Toggles;
        
        // Todo: Work in progress
        public bool AlwaysTriggers;
        public bool ResetOnceFinished;
    }
}