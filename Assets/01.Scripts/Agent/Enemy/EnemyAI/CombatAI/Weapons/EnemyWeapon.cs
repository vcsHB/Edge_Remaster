using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies.AI.Weapons
{

    public abstract class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] protected int _level;

        public UnityEvent OnAttackEvent;
        protected Enemy _owner;
        protected Collider2D _targetCollider;
        public virtual void SetLevel(int newLevel)
        {
            _level = Mathf.Clamp(newLevel, 1, 100);
        }


        public virtual void SetOwner(Enemy owner)
        {
            _owner = owner;

        }

        public void SetTarget(Collider2D targetCollider)
        {
            _targetCollider = targetCollider;
        }
        public void HandleAttack()
        {
            // 
            Attack();
        }
        protected abstract void Attack();

    }
}