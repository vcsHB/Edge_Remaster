using System.Linq;
using Combat;
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

    }
}