using System.Transactions;
using InteractSystem;
using UnityEngine;

namespace Agents.Players
{

    public class PlayerInteractController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private float _detectRadius;
        [SerializeField] private LayerMask _targetLayer;

        private IInteractable _currentInteractObject;
        private Player _player;
        [SerializeField] private bool _canInteract = true;

        public void AfterInit()
        {
        }

        public void Dispose()
        {
            _player.PlayerInput.OnInteractEvent -= HandleInteract;
        }

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _player.PlayerInput.OnInteractEvent += HandleInteract;
        }

        private void Update()
        {
            DetectInterctTarget();
        }


        private void DetectInterctTarget()
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, _detectRadius, _targetLayer);
            if(hit == null) return;
            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                if (_currentInteractObject == interactable)
                    return;

                if (_currentInteractObject != null )
                    _currentInteractObject.UnDetect();

                _currentInteractObject = interactable;
                _currentInteractObject.Detect();

            }
        }


        public void HandleInteract()
        {
            if(!_canInteract) return;
            if(_currentInteractObject == null) return;

            _currentInteractObject.Interact(_player);
            
        }
    }

    

}