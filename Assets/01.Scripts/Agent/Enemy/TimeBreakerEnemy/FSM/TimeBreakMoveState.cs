using UnityEngine;
namespace Agents.Enemies.FSM
{

    public class TimeBreakMoveState : EliteEnemyState
    {
        public TimeBreakMoveState(Enemy owner, EnemyStateMachine stateMachine, int animationParam) : base(owner, stateMachine, animationParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _enemyAI.StartMove();
            _enemyAI.MoveLogic.OnArriveEvent += HandleArrive;

        }

        public override void Update()
        {
            base.Update();
            _enemyAI.UpdateMove();
        }

        public override void Exit()
        {
            base.Exit();
            _enemyAI.EndMove();
            _enemyAI.MoveLogic.OnArriveEvent -= HandleArrive;

        }


        private void HandleArrive()
        {
            _mover.StopImmediately();
            _stateMachine.ChangeState("Idle");
        }

    }
}