using Combat;
using UnityEngine;
namespace Agents.Players.Combat
{
    public class PlayerBomb : PlayerWeapon
    {
        [SerializeField] private float _attackCooltime = 0.4f;
        private Caster _caster;
        private DamageCaster _damageCaster;
        [SerializeField] private ParticleSystem _attackVFX;
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
            _damageCaster.SetDamage(damage);
            _caster.Cast();
            _attackVFX.Play();
        }


        private void Awake()
        {
            _caster = GetComponent<Caster>();
            _damageCaster = GetComponent<DamageCaster>();

        }
    }
}