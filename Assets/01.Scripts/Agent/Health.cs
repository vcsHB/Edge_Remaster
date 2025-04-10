using System;
using Combat;
using UnityEngine;
using UnityEngine.Events;
namespace Agents
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        public UnityEvent OnHealthChangedEvent;
        public UnityEvent OnDamagedEvent;
        public Action<float, float> OnHealthValueChangedEvent;
        public UnityEvent OnDieEvent;

        public float MaxHealth => _maxHealth;
        private float _maxHealth;
        private float _currentHealth = 0;
        public bool isResist;

        public void Initialize(float health)
        {
            _maxHealth = health;
            SetMaxHealth();
        }

        public void SetMaxHealth()
        {
            _currentHealth = MaxHealth;
            InvokeHealthChange();

        }

        public void ApplyDamage(float damage)
        {
            if(isResist) return;
            _currentHealth -= damage;
            CheckDie();
            OnDamagedEvent?.Invoke();
            InvokeHealthChange();

        }

        public void Restore(float amount)
        {
            _currentHealth += amount;
            InvokeHealthChange();
        }
        private void CheckDie()
        {
            if (_currentHealth <= 0)
            {
                OnDieEvent?.Invoke();
            }
        }

        private void InvokeHealthChange()
        {
            OnHealthValueChangedEvent?.Invoke(_currentHealth, MaxHealth);
            OnHealthChangedEvent?.Invoke();
        }

    }
}