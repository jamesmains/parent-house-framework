using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace parent_house_framework.Cores {
    [Serializable]
    public abstract class TweenEffect : ObjectEffect {
        [SerializeField, FoldoutGroup("Settings")]
        protected Ease EaseIn = Ease.InOutSine;

        [SerializeField, FoldoutGroup("Settings")]
        protected Ease EaseOut = Ease.InOutSine;

        [SerializeField, FoldoutGroup("Settings")]
        protected float Duration;

        [SerializeField, FoldoutGroup("Settings")]
        protected float Overshoot;
    }

    [Serializable]
    public class TransformMoveEffect : TweenEffect {
        [SerializeField, FoldoutGroup("Settings")]
        private Vector3 StartPosition;

        [SerializeField, FoldoutGroup("Settings")]
        private Vector3 EndPosition;

#if UNITY_EDITOR
        [Button]
        public void SetStartPos() {
            StartPosition = AssociatedObject.transform.localPosition;
        }

        [Button]
        public void SetEndPos() {
            EndPosition = AssociatedObject.transform.localPosition;
        }
#endif

        public override void HandleState(bool state, Action callback) {
            base.HandleState(state, callback);
            AssociatedObject.transform.DOLocalMove(state ? EndPosition : StartPosition, Duration)
                .SetEase(state ? EaseIn : EaseOut, Overshoot);
        }
    }

    [Serializable]
    public class TransformRotateEffect : TweenEffect {
        [SerializeField, FoldoutGroup("Settings")]
        private RotateMode RotateMode;

        [SerializeField, FoldoutGroup("Settings")]
        private Vector3 StartRotation = Vector3.zero;

        [SerializeField, FoldoutGroup("Settings")]
        private Vector3 EndRotation = Vector3.one;

#if UNITY_EDITOR
        [Button]
        public void SetStartRotation() {
            // Todo: Test that this gets the actual value needed
            StartRotation = AssociatedObject.transform.eulerAngles;
        }

        [Button]
        public void SetEndRotation() {
            // Todo: Test that this gets the actual value needed
            EndRotation = AssociatedObject.transform.eulerAngles;
        }
#endif

        public override void HandleState(bool state, Action callback) {
            base.HandleState(state, callback);
            AssociatedObject.transform.DORotate(state ? EndRotation : StartRotation, Duration, RotateMode)
                .SetEase(state ? EaseIn : EaseOut, Overshoot);
        }
    }

    [Serializable]
    public class TransformScaleEffect : TweenEffect {
        [SerializeField, FoldoutGroup("Settings")]
        private Vector3 MinSize = Vector3.zero;

        [SerializeField, FoldoutGroup("Settings")]
        private Vector3 MaxSize = Vector3.one;
#if UNITY_EDITOR
        [Button]
        public void SetMinSize() {
            MinSize = AssociatedObject.transform.localScale;
        }

        [Button]
        public void SetMaxSize() {
            MaxSize = AssociatedObject.transform.localScale;
        }
#endif
        public override void HandleState(bool state, Action callback) {
            base.HandleState(state, callback);
            var move = AssociatedObject.transform.DOScale(state ? MaxSize : MinSize, Duration)
                .SetEase(state ? EaseIn : EaseOut, Overshoot);
            move.OnComplete(() => callback());
        }
    }


    [Serializable]
    public abstract class RectTweenEffect : TweenEffect {
        protected RectTransform m_RectTransform;

        public override void Initialize(GameObject parentObject) {
            base.Initialize(parentObject);
            m_RectTransform = AssociatedObject.GetComponent<RectTransform>();
        }
    }

    [Serializable]
    public class RectTransformScaleEffect : RectTweenEffect {
        [SerializeField, FoldoutGroup("Settings")]
        private Vector3 MinSize = Vector3.zero;

        [SerializeField, FoldoutGroup("Settings")]
        private Vector3 MaxSize = Vector3.one;
#if UNITY_EDITOR
        [Button]
        public void SetMinSize() {
            MinSize = m_RectTransform.localScale;
        }

        [Button]
        public void SetMaxSize() {
            MaxSize = m_RectTransform.localScale;
        }
#endif
        public override void HandleState(bool state, Action callback) {
            base.HandleState(state, callback);
            m_RectTransform.DOScale(state ? MaxSize : MinSize, Duration)
                .SetEase(state ? EaseIn : EaseOut, Overshoot);
        }
    }

    [Serializable]
    public class RectTransformRotateEffect : RectTweenEffect {
        [SerializeField, FoldoutGroup("Settings")]
        private RotateMode RotateMode;

        [SerializeField, FoldoutGroup("Settings")]
        private Vector3 StartRotation = Vector3.zero;

        [SerializeField, FoldoutGroup("Settings")]
        private Vector3 EndRotation = Vector3.one;

#if UNITY_EDITOR
        [Button]
        public void SetStartRotation() {
            // Todo: Test that this gets the actual value needed
            StartRotation = m_RectTransform.eulerAngles;
        }

        [Button]
        public void SetEndRotation() {
            // Todo: Test that this gets the actual value needed
            EndRotation = m_RectTransform.eulerAngles;
        }
#endif

        public override void HandleState(bool state, Action callback) {
            base.HandleState(state, callback);
            m_RectTransform.DORotate(state ? EndRotation : StartRotation, Duration, RotateMode)
                .SetEase(state ? EaseIn : EaseOut, Overshoot);
        }
    }

    [Serializable]
    public class RectTransformMoveEffect : RectTweenEffect {
        [SerializeField, FoldoutGroup("Settings")]
        private Vector3 StartPosition;

        [SerializeField, FoldoutGroup("Settings")]
        private Vector3 EndPosition;

#if UNITY_EDITOR
        [Button]
        public void SetStartPos() {
            StartPosition = m_RectTransform.anchoredPosition;
        }

        [Button]
        public void SetEndPos() {
            EndPosition = m_RectTransform.anchoredPosition;
        }
#endif

        public override void HandleState(bool state, Action callback) {
            base.HandleState(state, callback);
            m_RectTransform.DOAnchorPos3D(state ? EndPosition : StartPosition, Duration)
                .SetEase(state ? EaseIn : EaseOut, Overshoot);
        }
    }
}