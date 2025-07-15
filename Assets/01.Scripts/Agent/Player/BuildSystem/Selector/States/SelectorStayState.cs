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
                _selector.SelectorInput.OnInteractEvent += HnadleEditSelect;
                _infoPanel.SetStructure(_currentStructure);
            }
        }

        private void HnadleEditSelect()
        {
            _stateMachine.ChangeState(SelectorStateEnum.Edit);
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
                _selector.SelectorInput.OnInteractEvent -= HnadleEditSelect;
                _currentStructure.HandleStructureUnselected();
            }
            _optionSelector.Close();
        }


        private void HandleMove(Vector2 inputDirection)
        {
            _infoPanel.Close();
            _stateMachine.ChangeState(SelectorStateEnum.Move);
            _selector.SelectorInput.SetSelectPosition(_mover.HandleMove(inputDirection));
        }

        private void HandleGridSelect()
        {
            _stateMachine.ChangeState(SelectorStateEnum.Selected);

        }

    }
}