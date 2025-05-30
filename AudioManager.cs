using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace parent_house_framework {
    /// <summary>
    /// Things to add:
    /// Track fading
    /// Mixer Volume controls
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour {
        public static UnityEvent<AudioClip> OnPlayClip = new();

        [SerializeField, FoldoutGroup("Dependencies"),ReadOnly]
        private AudioSource audioSrc;

#if UNITY_EDITOR
        private void OnValidate() {
            if (!audioSrc) {
                audioSrc = GetComponent<AudioSource>();
            }
        }
#endif

        private void OnEnable() {
            OnPlayClip.AddListener(PlayClip);
        }

        private void OnDisable() {
            OnPlayClip.RemoveListener(PlayClip);
        }

        private void PlayClip(AudioClip clip) {
            if (clip == null) return;
            audioSrc.PlayOneShot(clip);
        }
    }
}