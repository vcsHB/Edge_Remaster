    using UnityEngine;
namespace Agents.Enemies.FSM
{

    public class EnemyMovingAttackState : EnemyMoveToTargetState
    {
        public EnemyMovingAttackState(Enemy owner, EnemyStateMachine stateMachine, int animationParam) : base(owner, stateMachine, animationParam)
        {
        }
    }
}