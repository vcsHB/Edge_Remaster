using Combat.Casters;
using UnityEngine;
namespace Agents.Enemies.AI.Weapons
{

    public class SelfExplosionWeapon : EnemyWeapon
    {
        [SerializeField] private Caster _explosionCaster;
        private DamageCaster[] _damageCasters;
        [SerializeField] private float[] _damageMultiplierList;

        private void Awake()
        {
            _damageCasters = GetComponentsInChildren<DamageCaster>();

            SetDamageMultiplier(_level);

        }

        protected override void Attack()
        {
            _explosionCaster.Cast();

            _owner.HandleAgentDie();
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