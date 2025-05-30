using System;
using System.Collections.Generic;
using System.Linq;
using parent_house_framework.Utils;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace parent_house_framework.UI.Notifications {
    public enum NotificationTypes {
        Toaster,
        ToasterSmall,
        Banner,
    }

    public class NotificationsManager : MonoBehaviour {
        [FormerlySerializedAs("NotificationTypes")] [SerializeField, FoldoutGroup("Settings")]
        private List<NotificationOption> NotificationOptions = new();

        public static Action<Notification> OnSendNotification;

        private void OnEnable() {
            OnSendNotification += HandleNotification;
        }

        private void OnDisable() {
            OnSendNotification -= HandleNotification;
        }

        private void HandleNotification(Notification notification) {
            var option = NotificationOptions.FirstOrDefault(o => o.Type == notification.Type);
            if (option == null) {
                UnityEngine.Debug.LogError("There is no notification type assigned to this notification");
                return;
            }

            var notificationObj = Pooler.Spawn(option.Prefab, option.Container);
        
            // Todo: replace the out variable with 'out NotificationDisplay notificationDisplay'
            if (notificationObj.TryGetComponent(out TextMeshProUGUI objText)) {
                objText.text = notification.Message;
            }
        }

        [Button]
        private void DebugSendToasterNotification(string msg) {
            Notification notification = new Notification(msg, NotificationTypes.Toaster);
            notification.Send();
        }

        [Button]
        private void DebugSendSmallToasterNotification(string msg) {
            Notification notification = new Notification(msg, NotificationTypes.ToasterSmall);
            notification.Send();
        }

        [Button]
        private void DebugSendBannerNotification(string msg) {
            Notification notification = new Notification(msg, NotificationTypes.Banner);
            notification.Send();
        }
    }

    [Serializable]
    public class NotificationOption {
        public NotificationTypes Type;
        public GameObject Prefab;
        public RectTransform Container;
    }

    public class Notification {
        public Notification(string msg, NotificationTypes notificationType) {
            Message = msg;
            Type = notificationType;
        }

        public NotificationTypes Type;
        public string Message;

        public void Send() {
            NotificationsManager.OnSendNotification?.Invoke(this);
        }
    }
}