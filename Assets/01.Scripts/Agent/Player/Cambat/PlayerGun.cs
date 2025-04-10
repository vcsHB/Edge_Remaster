using Combat;
using ObjectPooling;
using UnityEngine;
namespace Agents.Players.Combat
{
    public class PlayerGun : PlayerWeapon
    {
        [SerializeField] private float _attackCooltime = 0.4f;
        [SerializeField] private PlayerAim _aim;
        private float _currentTime;



        private void Update()
        {
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
            Vector2 direction = _aim.Position - (Vector2)transform.position;
            Bullet bullet = PoolManager.Instance.Pop(PoolingType.PlayerProjectile) as Bullet;
            bullet.transform.position = transform.position;
            bullet.transform.up = direction.normalized;

        }
    }
}