using Combat;
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

        protected Player _player;
        protected bool _isActive;
        public void Initialize(Player player)
        {
            _player = player;
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

        public virtual void Attack(float damage, float knockbackPower)
        {
            OnAttackEvent?.Invoke();
        }
    }
}