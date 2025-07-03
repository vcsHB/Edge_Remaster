using System;
using UnityEngine;
namespace BuildSystem.SelectorManage.FSM
{

    public class SelectorMoveState : SelectorState
    {
        public SelectorMoveState(GridSelector selector, SelectorStateMachine stateMachine) : base(selector, stateMachine)
        {
            _mover = selector.MoverCompo;
            _mover.OnArriveEvent += HandleMoveOver;
        }

        private void HandleMoveOver()
        {
            
            _stateMachine.ChangeState(SelectorStateEnum.Stay);
        }
    }
}