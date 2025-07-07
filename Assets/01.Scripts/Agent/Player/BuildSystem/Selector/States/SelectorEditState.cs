using System;
using BuildSystem.Structures;
using UnityEngine;
namespace BuildSystem.SelectorManage.FSM
{

    public class SelectorEditState : SelectorState
    {
        public SelectorEditState(GridSelector selector, SelectorStateMachine stateMachine) : base(selector, stateMachine)
        {
        }
        private Structure _currentStructure;
        private int _currentSelectIndex;
        private bool _canUpgradeStructure;


        public override void Enter()
        {
            base.Enter();
            _currentStructure = _selector.DetectStructure();

            _selector.SelectorInput.OnCancelEvent += HandleCancel;
            _selector.SelectorInput.OnBuildDestroyEvent += HandleDestroyStructure;

            _canUpgradeStructure = _currentStructure.DataSO.upgradeList.Length > 0;
            if (_canUpgradeStructure)
            {
                _selector.SelectorInput.OnSelectMoveEvent += HandleMove;
                _selector.SelectorInput.OnSelectEvent += HandleUpgrade;
                _selector.UpgradePanel.Open();
                _selector.UpgradePanel.SetUpgradeSlots(_currentStructure.DataSO.upgradeList);

            }
        }

        private void HandleMove(Vector2 direction)
        {// UpgradeSelect

            _selector.UpgradePanel.SelectSlot(_currentSelectIndex + (int)direction.x);
        }

        private void SelectUpgradeIndex(int index)
        {
            _selector.UpgradePanel.SelectSlot(index);
        }

        private void HandleCancel()
        {
            _stateMachine.ChangeState(SelectorStateEnum.Stay);
        }

        private void HandleDestroyStructure()
        {
            _infoPanel.Close();
            _infoPanel.Dispose();
            _currentStructure.DestroyStructure();
            _stateMachine.ChangeState(SelectorStateEnum.Stay);
        }

        private void HandleUpgrade()
        {

            HandleDestroyStructure();
        }

        public override void Exit()
        {
            base.Exit();
            _infoPanel.Close();
            if (_canUpgradeStructure)
            {
                _selector.SelectorInput.OnSelectMoveEvent -= HandleMove;
                _selector.SelectorInput.OnSelectEvent -= HandleUpgrade;
                _selector.UpgradePanel.Close();
                _selector.RequireResourcePanel.Close();
            }
            _selector.SelectorInput.OnCancelEvent -= HandleCancel;
            _selector.SelectorInput.OnBuildDestroyEvent -= HandleDestroyStructure;
        }


    }
}