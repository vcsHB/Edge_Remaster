using System;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }


        public override void Enter()
        {
            base.Enter();
            //_player.PlayerInput.OnMoveEvent += HandleMovement;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            HandleMovement(_player.PlayerInput.InputDirection);
        }


        public override void Exit()
        {
            base.Exit();
            //_player.PlayerInput.OnMoveEvent -= HandleMovement;
        }

        private void HandleMovement(Vector2 vector)
        {
            if(vector.magnitude < 0.1f) return;
            if(_mover.SetMoveTarget(vector))
                _stateMachine.ChangeState("Move");
        }
    }
}