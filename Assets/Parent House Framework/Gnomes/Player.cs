using System;
using Parent_House_Framework.Gnomes.Behaviors;
using Parent_House_Framework.Gnomes.Brains;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Parent_House_Framework.Gnomes {
    public class Player : MonoBehaviour {
        [SerializeField, FoldoutGroup("Dependencies")] 
        public Camera Cam;
    }
}



