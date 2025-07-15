using System.Linq;
using Combat;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
namespace BuildSystem.Structures
{

    public class Mender : EnergyRequireStructure
    {
        public UnityEvent OnMendEvent;
        [SerializeField] private Vector2 _detectRange;
        [SerializeField] private LayerMask _detectLayer;
        [SerializeField] private int _healTargetAmount = 3;
        [SerializeField] private float _healAmount = 30f;

        [Header("Visual Setting")]
        [SerializeField] private Transform _rangeVisualTrm;
        [SerializeField] private float _areaEnableDuration = 0.3f;
        [SerializeField] private float _visualScale = 3f;

        public override void HandleWaveStart()
        {
            base.HandleWaveStart();
            Mend();
        }

        [ContextMenu("DebugMend")]
        public void Mend()
        {
            if (_battery.TryUseEnergy(_requireEnergy))
            {
                Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, _detectRange, 0f, _detectLayer);

                Health[] healTargets = targets
                    .Select(collider => collider.GetComponent<Health>())
                    .Where(h => h != null && h.CurrentHealth < h.MaxHealth) // 100% 제외
                    .OrderBy(h => h.CurrentHealth) // 체력 낮은 순 정렬
                    .Take(_healTargetAmount) // 힐할 대상 수 제한
                    .ToArray();

                foreach (var target in healTargets)
                {
                    target.Restore(_healAmount);
                }
            }
            OnMendEvent?.Invoke();
        }

        public override void HandleStructureSelected()
        {
            _rangeVisualTrm.DOScale(_visualScale, _areaEnableDuration);
            base.HandleStructureSelected();
        }

        public override void HandleStructureUnselected()
        {
            base.HandleStructureUnselected();
            _rangeVisualTrm.DOScale(0f, _areaEnableDuration);
        }

    }
}