namespace parent_house_framework.Interactions {
    public enum NotifyState {
        Entry,
        Exit
    }

    public interface IInteractable {
        public void ChangeState();
        public void SetState(bool state, bool instant);
        public void Notify(NotifyState state);
        public bool RequireButtonToChangeState();
    }
}