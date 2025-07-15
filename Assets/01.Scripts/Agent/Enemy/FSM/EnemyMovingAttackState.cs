    using UnityEngine;
namespace Agents.Enemies.FSM
{

    public class EnemyMovingAttackState : EnemyMoveToTargetState
    {
        public EnemyMovingAttackState(Enemy owner, EnemyStateMachine stateMachine, int animationParam) : base(owner, stateMachine, animationParam)
        {
        }

        public override void Update()
        {
            base.Update();

            // thinking of this strategy Pattern...//
            //_enemyAI.UpdateAttack();
        }
    }
}