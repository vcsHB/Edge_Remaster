using System;
using BuildSystem.Structures;
using UnityEngine;

namespace BuildSystem.SelectorManage.FSM
{

    public class SelectorStayState : SelectorState
    {
        public SelectorStayState(GridSelector selector, SelectorStateMachine stateMachine) : base(selector, stateMachine)
        {
        }
        private bool _canBuildable;

        public override void Enter()
        {
            base.Enter();
            _selector.SelectorInput.OnSelectMoveEvent += HandleMove;
            Structure detectedStructure = _selector.DetectStructure();
            _canBuildable = detectedStructure == null;
            if (_canBuildable)
            {
                _selector.SelectorInput.OnSelectEvent += HandleGridSelect;
            }
            else
            {

                _infoPanel.SetStructure(detectedStructure);
            }
        }


        public override void Exit()
        {
            base.Exit();
            _selector.SelectorInput.OnSelectMoveEvent -= HandleMove;
            if (_canBuildable)
            {
                _selector.SelectorInput.OnSelectEvent -= HandleGridSelect;
            }
            _infoPanel.Close();
            _optionSelector.Close();
        }


        private void HandleMove(Vector2 inputDirection)
        {
            _stateMachine.ChangeState(SelectorStateEnum.Move);
            _mover.HandleMove(inputDirection);
        }

        private void HandleGridSelect()
        {
            _stateMachine.ChangeState(SelectorStateEnum.Selected);

        }

    }
}