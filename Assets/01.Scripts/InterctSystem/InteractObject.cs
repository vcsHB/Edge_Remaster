using Agents;
using UnityEngine;
using UnityEngine.Events;
namespace InteractSystem
{

    public class InteractObject : MonoBehaviour, IInteractable
    {
        public UnityEvent OnDetectedEvent;
        public UnityEvent OnInteractEvent;
        public UnityEvent OnUnDetectedEvent;
        [SerializeField] private bool _isOnceInteract;
        public bool CanInteract => _canInteract;
        [SerializeField] private bool _canInteract;

        public void Detect()
        {
            OnDetectedEvent?.Invoke();
        }

        public void Interact(Agent origin)
        {
            if(!_canInteract) return;
            if(_isOnceInteract)
                _canInteract = false;
            OnInteractEvent?.Invoke();

            
        }

        public void UnDetect()
        {
            OnUnDetectedEvent?.Invoke();
        }
    }
}