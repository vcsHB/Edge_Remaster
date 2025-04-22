using System;
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
        public override void Update()
        {
            base.Update();
            if(_enemyAI.IsTargeted)
                _stateMachine.ChangeState("Move");
        }

        public override void Exit()
        {
            base.Exit();

        }
    }
}