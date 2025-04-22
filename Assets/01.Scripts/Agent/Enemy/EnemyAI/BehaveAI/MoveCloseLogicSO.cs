using UnityEngine;
namespace Agents.Enemies.AI
{
    [CreateAssetMenu(menuName ="SO/EnemyAI/Movement/MoveCloseLogic")]
    public class MoveCloseLogicSO : MoveLogicSO
    { // Can't Arrive. get closer without stopping
        [SerializeField] private Color _gizmosColor = Color.red;
        public override void StartMove()
        {

        }

        public override void UpdateMove()
        {
            _mover.SetMovement(_detectData.targetDirection, _moveSpeed);
        }
        public override void EndMove()
        {
            _mover.StopImmediately();
        }

        public override void DrawGizmos()
        {
            Gizmos.color = _gizmosColor;

        }


        
    }
}