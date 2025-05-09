using Agents;
using Agents.Enemies;
using UnityEngine;
using UnityEngine.Events;
namespace Agnets.Enemies
{
    public abstract class EnemyAttackController : MonoBehaviour, IAgentComponent
    {
        public UnityEvent OnAttackEvent;
        protected Enemy _owner;
        [SerializeField] private float _attackCooltime;
        protected float _lastAttackTime;
        
        public virtual void HandleAttack()
        {
            if (_owner.IsDead) return;
            if (_attackCooltime + _lastAttackTime > Time.time) return;

            _lastAttackTime = Time.time;
            OnAttackEvent?.Invoke();
        }
        protected abstract void Attack();

        public void Initialize(Agent agent)
        {
            _owner = agent as Enemy;
        }

        public void AfterInit() { }

        public void Dispose() { }
    }
}