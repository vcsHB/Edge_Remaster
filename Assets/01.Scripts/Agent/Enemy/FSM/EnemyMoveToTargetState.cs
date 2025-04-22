using UnityEngine;
namespace Agents.Enemies.FSM
{

    public class EnemyMoveToTargetState : EnemyState
    {

        public EnemyMoveToTargetState(Enemy owner, EnemyStateMachine stateMachine, int animationParam) : base(owner, stateMachine, animationParam)
        {
        }

        public override void Enter()
        {
            base.Enter();

        }

        public override void Update()
        {
            base.Update();
            _mover.SetMovement(_enemyAI.TargetDirection);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}