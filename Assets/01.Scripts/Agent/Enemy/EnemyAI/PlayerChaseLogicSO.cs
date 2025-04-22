using UnityEngine;
namespace Agents.Enemies.AI
{

    public class PlayerChaseLogicSO : EnemyAILogicSO
    {
        private Collider2D _target;
        public override Transform GetTarget()
        {
            return _target.transform;
        }

        protected override void DetectTarget()
        {
            _target = Physics2D.OverlapCircle(_ownerTrm.position, _detectRadius, _targetLayer);
            if(_target == null) return;
            
        }
    }
}