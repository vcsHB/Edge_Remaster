using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerNoLimitMoveState : PlayerNoLimitState
    {
        public PlayerNoLimitMoveState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }

        public override void UpdateState()
        {
            base.UpdateState();
            Vector2 direction = _player.PlayerInput.InputDirection;
            //Debug.Log("리미트 모드 밍밍 Direction: " + direction);
            _mover.SetMovement(direction);

            if (direction.magnitude < 0.1f)
            {
                _stateMachine.ChangeState("NoLimitIdle");
            }
        }

    }
}