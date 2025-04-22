using Agents.Enemies.AI;
using UnityEngine;
namespace Agnets.Enemies.AI
{
    [CreateAssetMenu(menuName ="SO/EnemyAI/DetectSingleTarget")]
    public class DetectSingleTargetSO : EnemyAILogicSO
    {
        [SerializeField] private float _detectRadius = 0.4f;
        public override DetectData DetectTarget()
        {
            Collider2D target = Physics2D.OverlapCircle(_ownerTrm.position, _detectRadius, _whatIsTarget);

            bool isTargeted = target != null;

            if (isTargeted)
            {
                Transform targetTrm = target.transform;
                Vector2 direction = targetTrm.position - _ownerTrm.position;
                return new DetectData()
                {
                    isTargeted = true,
                    targetPos = targetTrm.position,
                    targetDirection = direction,
                    distanceToTarget = direction.magnitude
                };
            }
            else
            {
                return new DetectData()
                {
                    isTargeted = false,
                    targetPos = Vector2.zero,
                    targetDirection = Vector2.zero,
                    distanceToTarget = Mathf.Infinity
                };
            }
        }
    }
}