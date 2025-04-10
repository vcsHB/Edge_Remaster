using Agents;

namespace InteractSystem
{
    public interface IInteractable
    {
        public bool CanInteract { get; }
        public void Detect();
        public void Interact(Agent origin);
        public void UnDetect();
    }
}