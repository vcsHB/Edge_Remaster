using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies.AI.Weapons
{

    public abstract class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] protected int _level;

        public UnityEvent OnAttackEvent;
        protected Enemy _owner;

        public virtual void SetLevel(int newLevel)
        {
            _level = newLevel;
        }


        public void SetOwner(Enemy owner)
        {
            _owner = owner;

        }
        public void HandleAttack()
        {
            // 
            OnAttackEvent?.Invoke();
            Attack();
        }
        protected abstract void Attack();

    }
}