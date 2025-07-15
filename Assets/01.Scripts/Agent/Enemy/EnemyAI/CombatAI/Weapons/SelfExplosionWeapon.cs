using Combat.Casters;
using UnityEngine;

namespace Agents.Enemies.AI.Weapons
{
    [System.Serializable]
    public struct DamageScalingData
    {
        public DamageCaster caster;
        public float baseDamage;
        public AnimationCurve levelMultiplierCurve;

        public void Apply(int level)
        {
            if (caster == null) return;
            float multiplier = levelMultiplierCurve.Evaluate(level);
            caster.SetDamage(baseDamage * multiplier);
        }
    }

    public class SelfExplosionWeapon : EnemyWeapon
    {
        [SerializeField] private Caster _explosionCaster;
        [SerializeField] private DamageScalingData[] _damageScalingDataList;
        private bool _isAttacked;

        private void Awake()
        {
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

        private void SetDamageMultiplier(int level)
        {
            foreach (var data in _damageScalingDataList)
            {
                data.Apply(level);
            }
        }
    }
}
