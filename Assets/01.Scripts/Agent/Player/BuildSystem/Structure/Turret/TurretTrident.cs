using UnityEngine;
using System.Linq;
using Combat.Casters;
using UnityEngine.Events;

namespace BuildSystem.Structures.Turrets
{
    public class TurretTrident : TurretStructure
    {
        public UnityEvent OnDelayStartEvent;
        public UnityEvent OnDelayOverEvent;
        [SerializeField] private float _castAngle = 35f;
        [SerializeField] private float _fireDelay;
        [SerializeField] private Caster _mainCaster;
        [SerializeField] BurstAttackVisual _attackVFX;

        public override void Fire()
        {
            OnDelayStartEvent?.Invoke();
            Invoke(nameof(Shot), _fireDelay);


        }

        private void Shot()
        {
            Collider2D[] targets = _targetDetector.DetectAllTargets();
            Vector2 forward = _mainHead.transform.up;

            var filtered = targets.Where(target =>
            {
                Vector2 toTarget = ((Vector2)target.transform.position - (Vector2)_mainHead.transform.position).normalized;
                float angle = Vector2.Angle(forward, toTarget);
                return angle <= _castAngle * 0.5f;
            }).ToArray();
            _attackVFX.Play();
            OnDelayOverEvent?.Invoke();
            for (int i = 0; i < filtered.Length; i++)
            {
                _mainCaster.ForceCast(filtered[i]);
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            if (_mainHead == null) return;

            Vector3 origin = _mainHead.transform.position;
            Vector3 forward = _mainHead.transform.up;

            float halfAngle = _castAngle * 0.5f;

            Quaternion leftRot = Quaternion.AngleAxis(-halfAngle, Vector3.forward);
            Quaternion rightRot = Quaternion.AngleAxis(halfAngle, Vector3.forward);

            Vector3 leftDir = leftRot * forward;
            Vector3 rightDir = rightRot * forward;

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(origin, origin + leftDir * 5f);
            Gizmos.DrawLine(origin, origin + rightDir * 5f);
        }
#endif
    }
}
