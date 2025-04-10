using System.Collections;
using Managers;
using ObjectManage;
using UnityEngine;
namespace Agents.Players.FSM
{


    public class PlayerLimitState : PlayerNoLimitState
    {
        private MovePoint _targetPoint;
        private float _backDuration = 0.3f;

        public PlayerLimitState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }


        public override void Enter()
        {
            base.Enter();
             _attackController.SelectWeapon(0);
            _player.StartCoroutine(LimitCoroutine());
        }

        private IEnumerator LimitCoroutine()
        {
            _targetPoint = _mover.GetNearMovePoint();
            _mover.SetPreviousPos(_player.transform.position);
            _mover.SetMovePoint(_targetPoint);

            float currentTime = 0f;
            while (currentTime <= _backDuration)
            {
                currentTime += Time.deltaTime;
                _mover.SetMovement(currentTime / _backDuration);
                yield return null;
            }
            _mover.SetEdgeMode(true);
            _mover.StopImmediately();
            _limiter.SetLimit();
            _stateMachine.ChangeState("Idle");
            ScoreManager.Instance.SetEndNoLimit();
        }
    }
}