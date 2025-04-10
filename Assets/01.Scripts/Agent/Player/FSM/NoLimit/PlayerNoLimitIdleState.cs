using UnityEngine;
namespace Agents.Players.FSM
{


    public class PlayerNoLimitIdleState : PlayerNoLimitState
    {
        public PlayerNoLimitIdleState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately();
        }


        public override void UpdateState()
        {
            base.UpdateState();
            Vector2 direction = _player.PlayerInput.InputDirection;
            if(direction.magnitude> 0.1f)
            {
                _stateMachine.ChangeState("NoLimitMove");
            }
        }

        public override void Exit()
        {
            base.Exit();
        }


    }
}