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
        private bool _isBlocked;
        private Structure _currentStructure;

        public override void Enter()
        {
            base.Enter();
            _selector.SelectorInput.OnSelectMoveEvent += HandleMove;
            _isBlocked = _selector.DetectObstacle();
            if (_isBlocked) return;

            _currentStructure = _selector.DetectStructure();
            _canBuildable = _currentStructure == null;
            if (_canBuildable)
            {
                _selector.SelectorInput.OnSelectEvent += HandleGridSelect;
            }
            else
            {
                _currentStructure.HandleStructureSelected();
                //_selector.SelectorInput.OnSelectEvent += HandleEditSelect;
                _infoPanel.SetStructure(_currentStructure);
            }
        }


        public override void Exit()
        {
            base.Exit();
            _selector.SelectorInput.OnSelectMoveEvent -= HandleMove;
            if (_isBlocked) return;

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
            _selector.SelectorInput.SetSelectPosition(_mover.HandleMove(inputDirection));
        }

        private void HandleGridSelect()
        {
            _stateMachine.ChangeState(SelectorStateEnum.Selected);

        }

    }
}