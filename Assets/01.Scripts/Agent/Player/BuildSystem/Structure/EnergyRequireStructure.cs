using System;
using BuildSystem.ResourceManage;
using UnityEngine;
namespace BuildSystem.Structures
{

    public class EnergyRequireStructure : Structure
    {
        public event Action<bool> OnEnergyEnoughEvent;
        [SerializeField] protected float _requireEnergy = 10f;
        protected Battery _battery;
        private EnergyVisualRenderer[] _energyVisuals;

        protected override void Awake()
        {
            base.Awake();
            _battery = GetComponent<Battery>();
            _battery.OnEnergyValueChangedEvent += HandleEnergyValueChange;

            _energyVisuals = GetComponentsInChildren<EnergyVisualRenderer>();
            for (int i = 0; i < _energyVisuals.Length; i++)
            {
                OnEnergyEnoughEvent += _energyVisuals[i].SetVisualEnable;
            }
        }

        private void HandleEnergyValueChange(float currentEnergy)
        {
            OnEnergyEnoughEvent?.Invoke(currentEnergy >= _requireEnergy);
        }
    }
}