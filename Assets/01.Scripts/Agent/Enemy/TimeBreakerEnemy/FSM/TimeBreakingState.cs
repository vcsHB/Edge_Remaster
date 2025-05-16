using System;
using UnityEngine;
namespace Agents.Enemies.FSM
{

    public class TimeBreakingState : EliteEnemyState
    {

        private TimeBreakerMovement _timeBreakerMover;


        public TimeBreakingState(Enemy owner, EnemyStateMachine stateMachine, int animationParam) : base(owner, stateMachine, animationParam)
        {
            _timeBreakerMover = owner.GetCompo<TimeBreakerMovement>();
        }

        public override void Enter()
        {
            base.Enter();
            _eliteAI.AbilitySO.HandleUseAbility(HandleMoveComplete);
        }

        private void HandleMoveComplete()
        {
            // State Change.
            _stateMachine.ChangeState("Idle");
        }
    }
}