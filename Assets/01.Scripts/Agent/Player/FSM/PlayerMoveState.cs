using UnityEngine;
namespace Agents.Players.FSM
{


    public class PlayerMoveState : PlayerState
    {
        private float _currentMoveTime = 0f;
        private float _moveDuration = 0.4f;        

        public PlayerMoveState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _currentMoveTime = 0f;
            _mover.SetPreviousPos(_player.transform.position);
            _moveDuration = 0.01f * _mover.MoveDistance;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _currentMoveTime += Time.deltaTime * _player.PlayerStatus.edgeSlideSpeed.GetValue();
            _mover.SetMovement(_currentMoveTime / _moveDuration);
            if(_currentMoveTime > _moveDuration)
                _stateMachine.ChangeState("Idle");
        }

        public override void Exit()
        {
            base.Exit();
        }


    }
}