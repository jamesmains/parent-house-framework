using Sirenix.OdinInspector;

namespace Parent_House_Framework {
    public abstract class EventPropagator: SerializedScriptableObject
    {
        public abstract void Invoke();
    }
}