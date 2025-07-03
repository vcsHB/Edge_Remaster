using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace BuildSystem.Structures
{
    public class Pump : Structure
    {
        public UnityEvent OnPumpEnergyEvent;
        [SerializeField] private LayerMask _structureLayer;
        [SerializeField] private Vector2 _detectArea;

        [Header("Pump Setting")]
        [SerializeField] private float _fillEnergyAmount = 10f;

        [Header("Visual Setting")]
        [SerializeField] private Transform _rangeVisualTrm;
        [SerializeField] private float _areaEnableDuration = 0.3f;

        private SpriteRenderer _rangeRenderer;

        protected override void Awake()
        {
            base.Awake();
            _rangeRenderer = _rangeVisualTrm.GetComponent<SpriteRenderer>();
            _rangeRenderer.size = _detectArea;
        }

        public override void HandleWaveStart()
        {
            base.HandleWaveStart();
            PumpEnergy();
        }
        [ContextMenu("PumpEnergy")]
        public void PumpEnergy()
        {
            var energyOwners = GetEnergyOwnersInRange();
            int targetCount = energyOwners.Length;
            if (targetCount == 0) return;

            float distributedEnergy = _fillEnergyAmount / targetCount;
            foreach (var owner in energyOwners)
            {
                owner.RestoreEnergy(distributedEnergy);
            }
            OnPumpEnergyEvent?.Invoke();
        }

        public override void HandleStructureSelected()
        {
            base.HandleStructureSelected();
            SetEnergyOwnersHighlight(true);
            _rangeVisualTrm.DOScale(1f, _areaEnableDuration);
        }

        public override void HandleStructureUnselected()
        {
            base.HandleStructureUnselected();
            SetEnergyOwnersHighlight(false);
            _rangeVisualTrm.DOScale(0f, _areaEnableDuration);
        }

        private IEnergyRestorable[] GetEnergyOwnersInRange()
        {
            return Physics2D.OverlapBoxAll(transform.position, _detectArea, 0, _structureLayer)
                .Select(collider => collider.GetComponent<IEnergyRestorable>())
                .Where(component => component != null)
                .ToArray();
        }

        private void SetEnergyOwnersHighlight(bool highlight)
        {
            var energyOwners = GetEnergyOwnersInRange();
            foreach (var owner in energyOwners)
            {
                owner.SetHighlight(highlight);
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _detectArea);
        }

#endif
    }
}
