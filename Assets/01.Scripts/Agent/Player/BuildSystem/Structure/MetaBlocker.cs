using UnityEngine;
using UnityEngine.Events;

namespace BuildSystem.Structures
{

    public class MetaBlocker : EnergyRequireStructure
    {

        public UnityEvent OnReturnSuccessEvent;
        [SerializeField, Range(0f, 1f)] private float _damageIgnoreRate;
        [SerializeField, Range(0f, 1f)] private float _returnRatio = 0.5f;
        protected override void Awake()
        {
            base.Awake();
            HealthCompo.OnHealthDecreaseValueEvent += HandleHealthChanged;
        }

        private void HandleHealthChanged(float currentHealth, float maxHealth, float damage)
        {
            if (_battery.TryUseEnergy(_requireEnergy))
            {

                if (_damageIgnoreRate > Random.Range(0f, 1f))
                {
                    HealthCompo.Restore(damage * _returnRatio);
                    OnReturnSuccessEvent?.Invoke();
                }
            }
        }
    }
}