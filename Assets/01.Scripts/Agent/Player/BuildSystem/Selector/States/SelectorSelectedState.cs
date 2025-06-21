using System;
using UnityEngine;
namespace BuildSystem.SelectorManage.FSM
{

    public class SelectorSelectedState : SelectorState
    {
        public SelectorSelectedState(GridSelector selector, SelectorStateMachine stateMachine) : base(selector, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _selector.SelectorInput.OnSelectMoveEvent += HandleMove;
            _selector.SelectorInput.OnSelectEvent += HandleSelect;
        }

        private void HandleSelect()
        {
        }

        private void HandleMove(Vector2 inputDirection)
        {
            _optionSelector.Move(inputDirection);
        }
    }
}