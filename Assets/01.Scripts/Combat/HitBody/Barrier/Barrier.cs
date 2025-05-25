using System;
using UnityEngine;
using UnityEngine.Events;

namespace Combat.CombatObjects
{

    public class Barrier : MonoBehaviour, IDamageable
    {
        public UnityEvent OnBarrierBreakEvent;
        public UnityEvent OnBarrierDamagedEvent;

        public Action<float, float> OnBarrierDurabilityChangedEvent;
        [SerializeField] private float _barrierMaxDurability = 10;
        [SerializeField] private float _currentDurability = 10;
        [SerializeField] private float _hitResistanceCooltime = 0.15f;
        private BarrierVisual _barrierVisual;
        private Collider2D _collider;
        private bool _barrierEnable;
        private float _lastHitTime;

        private void Awake()
        {
            _barrierVisual = GetComponentInChildren<BarrierVisual>();
            _collider = GetComponent<Collider2D>();
            OnBarrierDurabilityChangedEvent += _barrierVisual.HandleBarrierDurabilityChange;
        }

        private void Start()
        {
            ResetBarrier();

        }
        private void OnDestroy()
        {
            OnBarrierDurabilityChangedEvent -= _barrierVisual.HandleBarrierDurabilityChange;

        }

        public void SetMaxDurability()
        {
            _currentDurability = _barrierMaxDurability;
        }
        public bool ApplyDamage(CombatData data)
        {
            if (!data.invalidityResistance && _lastHitTime + _hitResistanceCooltime > Time.time) return false;
            _currentDurability -= data.damage;
            _barrierVisual.StartHitEffect(data.originPosition);
            _lastHitTime = Time.time;
            CheckBarrierBreak();
            InvokeBarrierDurabilityChange();
            return true;

        }

        private void InvokeBarrierDurabilityChange()
        {
            OnBarrierDurabilityChangedEvent?.Invoke(_currentDurability, _barrierMaxDurability);
            OnBarrierDamagedEvent?.Invoke();
        }

        private void CheckBarrierBreak()
        {
            if (!_barrierEnable) return;

            if (_currentDurability <= 0)
            {
                OnBarrierBreakEvent?.Invoke();
                _collider.enabled = false;
                _barrierEnable = false;
                _barrierVisual.SetVisualEnable(false);
            }
        }

        public void ResetBarrier()
        {
            SetMaxDurability();
            _collider.enabled = true;
            _barrierEnable = true;
            _barrierVisual.SetVisualEnable(true);
        }
    }

}