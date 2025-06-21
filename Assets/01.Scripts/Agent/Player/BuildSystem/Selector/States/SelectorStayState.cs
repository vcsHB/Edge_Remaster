using System;
using UnityEngine;

namespace BuildSystem.SelectorManage.FSM
{

    public class SelectorStayState : SelectorState
    {
        public SelectorStayState(GridSelector selector, SelectorStateMachine stateMachine) : base(selector, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _selector.SelectorInput.OnSelectMoveEvent += HandleMove;
            _selector.SelectorInput.OnSelectEvent += HandleSelect;

        }


        public override void Exit()
        {
            base.Exit();
            _selector.SelectorInput.OnSelectMoveEvent -= HandleMove;
            _selector.SelectorInput.OnSelectEvent -= HandleSelect;

        }


        private void HandleMove(Vector2 inputDirection)
        {
            _stateMachine.ChangeState(SelectorStateEnum.Move);
            _mover.HandleMove(inputDirection);
        }

        private void HandleSelect()
        {
            _stateMachine.ChangeState(SelectorStateEnum.Selected);
            
        }

    }
}