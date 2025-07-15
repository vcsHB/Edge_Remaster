using System;
using BuildSystem.ResourceManage;
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
        private EnergyDisplayer _energyDisplayer;
        public float MaxEnergy => _maxEnergy;
        public float CurrentEnergy => _currentEnergy;
        public bool IsEnough(float amount) => _currentEnergy >= amount;


        private void Awake()
        {
            _energyDisplayer = GetComponentInChildren<EnergyDisplayer>();
            if (_energyDisplayer == null)
            {
                Debug.LogError($"Not Exist EnergyDisplayer Script in battery Object. ObjectName:{gameObject.name}");
                return;
            }

            OnEnergyRestoreEvent += _energyDisplayer.SetEnergyAmount;
            OnEnergyUseEvent += _energyDisplayer.SetEnergyAmount;
        }
        public void SetEnergy(float amount)
        {
            _currentEnergy = Mathf.Clamp(amount, 0f, _maxEnergy);
            OnEnergyValueChangedEvent?.Invoke(_currentEnergy);
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

        public void SetHighlight(bool value)
        {
            _energyDisplayer.SetEnable(value);
        }

        public void SetCurrentEnergy(float energy)
        {
            _currentEnergy = energy;

        }
    }
}