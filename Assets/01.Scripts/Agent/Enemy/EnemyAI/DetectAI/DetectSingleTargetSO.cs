using Agents.Enemies.AI;
using UnityEngine;

namespace Agnets.Enemies.AI
{
    [CreateAssetMenu(menuName = "SO/EnemyAI/Detect/DetectSingleTarget")]
    public class DetectSingleTargetSO : DetectLogicSO
    {
        [SerializeField] private float _detectRadius = 0.4f;

        public override DetectData DetectTarget()
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(_ownerTrm.position, _detectRadius, _whatIsTarget);

            if (targets.Length == 0)
            {
                DetectData emptyData = new DetectData
                {
                    isTargeted = false,
                    targetPos = Vector2.zero,
                    targetDirection = Vector2.zero,
                    distanceToTarget = Mathf.Infinity,
                    targetCollider = null
                };
                InvokeDetectEvent(emptyData);
                return emptyData;
            }

            // Nearest
            Collider2D closestTarget = null;
            float minDist = float.MaxValue;
            Vector2 origin = _ownerTrm.position;

            foreach (var target in targets)
            {
                float dist = Vector2.Distance(origin, target.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestTarget = target;
                }
            }

            Vector2 directionToTarget = (Vector2)closestTarget.transform.position - origin;

            DetectData data = new DetectData
            {
                isTargeted = true,
                targetPos = closestTarget.transform.position,
                targetDirection = directionToTarget,
                distanceToTarget = minDist,
                targetCollider = closestTarget
            };
            InvokeDetectEvent(data);
            return data;
        }
    }
}
