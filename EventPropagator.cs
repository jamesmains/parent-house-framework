using Sirenix.OdinInspector;

namespace parent_house_framework {
    public abstract class EventPropagator: SerializedScriptableObject
    {
        public abstract void Invoke();
    }
}