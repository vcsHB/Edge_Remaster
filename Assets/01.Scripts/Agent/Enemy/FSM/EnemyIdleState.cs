    using UnityEngine;
namespace Agents.Enemies.FSM
{

    public class EnemyIdleState : EnemyState
    {
        public EnemyIdleState(Enemy owner, EnemyStateMachine stateMachine, int animationParam) : base(owner, stateMachine, animationParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately();
        }
    }
}