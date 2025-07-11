using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
namespace Agents.Enemies.AI.Weapons
{

    public class ShooterWeapon : EnemyWeapon
    {
        [SerializeField] private ProjectileShooter _shooter;
        [SerializeField] private float _attackCooltime = 1f;
        private float _nextAttackTime;

        private void Update()
        {
            if (Time.time >= _nextAttackTime)
            {
                _nextAttackTime = Time.time + _attackCooltime;
                Attack();
            }
        }
        protected override void Attack()
        {
            if (_targetCollider == null) return;
            Vector2 direction = _targetCollider.transform.position - _owner.transform.position;
            _shooter.FireProjectile(direction.normalized);
        }
    }
}