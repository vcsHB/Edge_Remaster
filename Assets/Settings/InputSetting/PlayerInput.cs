using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputManage
{
    [CreateAssetMenu(menuName = "SO/Input/PlayerInput")]
    public class PlayerInput : ScriptableObject, Controls.IPlayerActions
    {
        public event Action OnAttackEvent;
        public event Action OnInteractEvent;
        public event Action<Vector2> OnMoveEvent;
        public event Action OnUseSkill1Event;
        public event Action OnUseSkill2Event;
        public Vector2 InputDirection { get; private set; }
        public Vector2 MousePosition { get; private set; }


        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnAttackEvent?.Invoke();
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnInteractEvent?.Invoke();
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            InputDirection = context.ReadValue<Vector2>();
            if (context.performed)
            {
                OnMoveEvent?.Invoke(InputDirection);
            }
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            MousePosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        }

        public void OnUseSkill1(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnUseSkill1Event?.Invoke();
        }

        public void OnUseSkill2(InputAction.CallbackContext context)
        {
             if(context.performed)
                OnUseSkill2Event?.Invoke();
        }
    }

}