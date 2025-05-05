using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
namespace Agents.Players.Combat
{
    public class PlayerShooter : PlayerWeapon
    {
        [SerializeField] private float _attackCooltime = 0.4f;
        private ProjectileShooter _shooter;

        private float _currentTime;

        protected void Awake()
        {
            _shooter = GetComponentInChildren<ProjectileShooter>();
        }


        protected override void Update()
        {
            base.Update();
            if (!_isActive) return;
            _currentTime += Time.deltaTime;

            if (_currentTime > _attackCooltime)
            {
                _currentTime = 0f;
                float damage = _player.PlayerStatus.attackDamage.GetValue();
                Attack(damage, 10f);
            }
        }

        public override void Attack(float damage, float knockbackPower)
        {
            base.Attack(damage, knockbackPower);
            _shooter.SetDirection(AimDirection);
            _shooter.FireProjectile();
        }
    }
}