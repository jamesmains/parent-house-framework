using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Todo: Make more readable -- pretty hard to sort through
// |-> Either individual files or group by category

namespace parent_house_framework.Cores {
    public class JuicyCore : MonoBehaviour {
    }

    [Serializable]
    public abstract class ObjectEffect {
        protected GameObject AssociatedObject;
        protected Action Callback;
        public virtual void Initialize(GameObject selfObject) {
            AssociatedObject = selfObject;
        }

        public virtual void HandleState(bool state, Action callback) {
            Callback = callback;
        }

        public virtual void HandleCallbacks() {
            Callback.Invoke();
        }
    }

    [Serializable]
    public class AnimationBoolObjectEffect : ObjectEffect {
        private Animator Anim;
        public string BoolName;

        public override void Initialize(GameObject parentObject) {
            base.Initialize(parentObject);
            Anim = AssociatedObject.GetComponent<Animator>();
        }

        public override void HandleState(bool state, Action callback) {
            base.HandleState(state, callback);
            Anim.SetBool(BoolName, state);
        }
    }

    [Serializable]
    public class GameObjectActiveObjectEffect : ObjectEffect {

        public override void HandleState(bool state, Action callback) {
            base.HandleState(state, callback);
            AssociatedObject.SetActive(state);
        }
    }

    [Serializable]
    public class ImageSpriteSwapEffect : ObjectEffect {
        [SerializeField, FoldoutGroup("Settings")]
        private Sprite OnSprite;

        [SerializeField, FoldoutGroup("Settings")]
        private Sprite OffSprite;

        private Image m_Image;

        public override void Initialize(GameObject parentObject) {
            base.Initialize(parentObject);
            m_Image = AssociatedObject.GetComponent<Image>();
        }

        public override void HandleState(bool state, Action callback) {
            base.HandleState(state, callback);
            m_Image.sprite = state ? OnSprite : OffSprite;
        }
    }

    [Serializable]
    public class SetTextGUIMessageEffect : ObjectEffect {
        [SerializeField, FoldoutGroup("Settings")]
        private string OnMessage;

        [SerializeField, FoldoutGroup("Settings")]
        private string OffMessage;

        private TextMeshProUGUI m_TextMeshProUGUI;

        public override void Initialize(GameObject parentObject) {
            base.Initialize(parentObject);
            m_TextMeshProUGUI = AssociatedObject.GetComponent<TextMeshProUGUI>();
        }

        public override void HandleState(bool state, Action callback) {
            base.HandleState(state, callback);
            m_TextMeshProUGUI.text = state ? OnMessage : OffMessage;
        }
    }

    [Serializable]
    public class EventTriggerEffect : ObjectEffect {
        [SerializeField, FoldoutGroup("Settings")]
        private UnityEvent OnEvent = new();

        [SerializeField, FoldoutGroup("Settings")]
        private UnityEvent OffEvent = new();

        public override void HandleState(bool state, Action callback) {
            if (state) {
                OnEvent?.Invoke();
            }
            else {
                OffEvent?.Invoke();
            }
        }
    }

    [Serializable]
    public class CanvasFadeEffect : ObjectEffect {
        [SerializeField, FoldoutGroup("Settings")]
        private Vector2 OpacityRange = Vector2.up;

        [SerializeField, FoldoutGroup("Settings")]
        private float Duration = 0.25f;

        private CanvasGroup m_CanvasGroup;

        [Button]
        private void SetAlpha(float alpha) {
            m_CanvasGroup.alpha = alpha;
        }

        public override void Initialize(GameObject parentObject) {
            base.Initialize(parentObject);
            m_CanvasGroup = AssociatedObject.GetComponent<CanvasGroup>();
        }

        public override void HandleState(bool state, Action callback) {
            base.HandleState(state, callback);
            m_CanvasGroup.DOFade(state ? OpacityRange.y : OpacityRange.x, Duration);
        }
    }

}