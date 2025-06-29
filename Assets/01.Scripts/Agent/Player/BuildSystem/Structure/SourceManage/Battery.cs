using System;
using UnityEngine;
namespace BuildSystem.Structures
{

    public class Battery : MonoBehaviour, IEnergyRestorable
    {
        public event Action<float, float> OnEnergyRestoreEvent;
        public event Action<float, float> OnEnergyUseEvent;
        public event Action<float> OnEnergyValueChangedEvent;

        [SerializeField] private float _maxEnergy;
        [SerializeField] private float _currentEnergy;
        public float MaxEnergy => _maxEnergy;
        public float CurrentEnergy => _currentEnergy;
        public bool IsEnough(float amount) => _currentEnergy >= amount;


        private void Awake()
        {

        }


        public bool TryUseEnergy(float amount)
        {
            if (!IsEnough(amount)) return false;

            _currentEnergy -= amount;
            OnEnergyUseEvent?.Invoke(_currentEnergy, _maxEnergy);
            OnEnergyValueChangedEvent?.Invoke(_currentEnergy);
            return true;

        }

        public void RestoreEnergy(float amount)
        {
            _currentEnergy += amount;
            _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _maxEnergy);
            OnEnergyRestoreEvent?.Invoke(_currentEnergy, _maxEnergy);
            OnEnergyValueChangedEvent?.Invoke(_currentEnergy);
        }
    }
}