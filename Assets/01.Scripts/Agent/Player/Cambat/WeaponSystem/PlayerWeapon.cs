using InputManage;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players.Combat
{

    public abstract class PlayerWeapon : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent OnAttackEvent;
        public UnityEvent OnSelectEvent;
        [Header("Setting Values")]
        [SerializeField] protected float _normalDamage;
        public Vector2 AimDirection { get; protected set; }
        public Vector2 AimPosition { get; protected set; }

        protected Player _player;
        protected bool _isActive;
        protected PlayerInput _playerInput;
        public void Initialize(Player player)
        {
            _player = player;
            _playerInput = player.PlayerInput;

        }

        public virtual void SetEnabled()
        {
            _isActive = true;
            OnSelectEvent?.Invoke();
        }
        public virtual void SetDisable()
        {
            _isActive = false;
        }

        protected virtual void Update()
        {
            AimPosition = _playerInput.MousePosition;
            AimDirection = AimPosition - (Vector2)transform.position;
        }

        public virtual void Attack(float damage, float knockbackPower)
        {
            OnAttackEvent?.Invoke();
        }
    }
}