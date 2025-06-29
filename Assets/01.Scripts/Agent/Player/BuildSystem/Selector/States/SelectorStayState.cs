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
        private Structure _currentStructure;

        public override void Enter()
        {
            base.Enter();
            _selector.SelectorInput.OnSelectMoveEvent += HandleMove;
            _currentStructure = _selector.DetectStructure();
            _canBuildable = _currentStructure == null;
            if (_canBuildable)
            {
                _selector.SelectorInput.OnSelectEvent += HandleGridSelect;
            }
            else
            {
                _currentStructure.HandleStructureSelected();
                _infoPanel.SetStructure(_currentStructure);
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
            if (_currentStructure != null)
            {
                _currentStructure.HandleStructureUnselected();
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