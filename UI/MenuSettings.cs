using System;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace parent_house_framework.UI {
    [Serializable]
    public class MenuSettings {
        public MenuState InitialState = MenuState.Open;
        public bool InstantOnEnable = true;
        public float OpenTime = 1f;
        public float CloseTime = 1f;

        [FoldoutGroup("Events")] public UnityEvent OnStartOpen = new();
        [FoldoutGroup("Events")] public UnityEvent OnFinishOpen = new();
        [FoldoutGroup("Events")] public UnityEvent OnStartClose = new();
        [FoldoutGroup("Events")] public UnityEvent OnFinishClose = new();
    }
}