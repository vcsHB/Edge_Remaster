using NUnit.Framework;
using UnityEngine;
namespace Agents.Enemies.AI
{
    [CreateAssetMenu(menuName = "SO/EnemyAI/Movement/KeepDistanceCloseLogic")]
    public class MoveKeepDistanceCloseLogicSO : MoveLogicSO
    {
        [SerializeField] private float _ignoreDistance = 0.4f;
        [SerializeField] private Color _ignoreDistanceColor;
        [SerializeField] private Color _detectGizmosColor;

        public override void StartMove()
        {
        }

        public override void UpdateMove()
        {
            _mover.SetMovement(_detectData.targetDirection, _moveSpeed);
            CheckArrive();
        }
        public override void EndMove()
        {
            _mover.StopImmediately();
        }
        private void CheckArrive()
        {
            if (_detectData.distanceToTarget <= _ignoreDistance)
            {
                Debug.Log("도착");
                Arrive();
            }
        }

        public override void DrawGizmos()
        {
            if (_ownerTrm == null) return;
            Gizmos.color = _ignoreDistanceColor;
            Gizmos.DrawWireSphere(_ownerTrm.position, _ignoreDistance);
            Gizmos.color = _detectGizmosColor;
            Gizmos.DrawLine(_ownerTrm.position, _detectData.targetPos);
        }

    }
}