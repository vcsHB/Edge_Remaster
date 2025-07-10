using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
namespace Agents.Enemies.AI.Weapons
{

    public class MoveShooterWeapon : EnemyWeapon
    {
        [SerializeField] private ProjectileShooter _shooter;
        protected override void Attack()
        {
            if (_targetCollider == null) return;
            Vector2 direction = _targetCollider.transform.position - _owner.transform.position;
            _shooter.FireProjectile(direction.normalized);
        }
    }
}