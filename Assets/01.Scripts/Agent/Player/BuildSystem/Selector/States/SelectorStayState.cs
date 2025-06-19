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

        }

        private void HandleArrive()
        {
        }

        private void HandleMove(Vector2 inputDirection)
        {

        }
    }
}