using System;
using Combat.Casters;
using UnityEngine;
namespace Agents.Enemies.AI.Weapons
{

    public class SelfExplosionWeapon : EnemyWeapon
    {
        [SerializeField] private Caster _explosionCaster;
        private DamageCaster[] _damageCasters;
        [SerializeField] private float[] _damageMultiplierList;
        private bool _isAttacked;
        private void Awake()
        {
            _damageCasters = GetComponentsInChildren<DamageCaster>();
            SetDamageMultiplier(_level);

        }

        public override void SetOwner(Enemy owner)
        {
            base.SetOwner(owner);
            owner.OnGeneratedEvent += HandleEnemyGenerated;
        }

        private void HandleEnemyGenerated()
        {
            _isAttacked = false;
        }

        protected override void Attack()
        {
            if (!_isAttacked)
            {
                _explosionCaster.Cast();
                _owner.HandleAgentDie();
                OnAttackEvent?.Invoke();
                _isAttacked = true;
            }
        }

        public override void SetLevel(int newLevel)
        {
            base.SetLevel(newLevel);
            SetDamageMultiplier(newLevel);

        }

        private void SetDamageMultiplier(int newLevel)
        {
            for (int i = 0; i < _damageCasters.Length; i++)
            {
                _damageCasters[i].SetDamage(_damageMultiplierList[i] * newLevel);
            }

        }
    }
}