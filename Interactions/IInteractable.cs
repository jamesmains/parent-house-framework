namespace parent_house_framework.Interactions {
    public enum NotifyState {
        Entry,
        Exit
    }

    public interface IInteractable {
        public void ChangeState();
        public void SetState(bool state);
        public void Notify(NotifyState state);
        public bool RequireButtonToChangeState();
    }
}