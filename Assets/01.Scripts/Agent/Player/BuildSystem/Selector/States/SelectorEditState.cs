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
        private StructureDataSO _structureData;
        private int _currentSelectIndex;
        private bool _canUpgradeStructure;


        public override void Enter()
        {
            base.Enter();
            _currentStructure = _selector.DetectStructure();
            _currentStructure.OnDestroyEvent += HandleDestroyUnSelect;
            _structureData = _currentStructure.DataSO;
            if (_structureData == null)
            {
                Debug.LogError("Not Contain StructureData SO in Structure Prefab");
            }
            _selector.SelectorInput.OnCancelEvent += HandleCancel;
            _selector.SelectorInput.OnBuildDestroyEvent += HandleDestroyStructure;
            _currentSelectIndex = 0;
            _canUpgradeStructure = _structureData.upgradeList.Length > 0;
            if (_canUpgradeStructure)
            {
                _selector.SelectorInput.OnSelectMoveEvent += HandleMove;
                _selector.SelectorInput.OnSelectEvent += HandleUpgrade;
                _selector.UpgradePanel.Open();
                _selector.UpgradePanel.SetUpgradeSlots(_structureData.upgradeList);
                SelectUpgradeIndex(_currentSelectIndex);
            }
        }

        private void HandleDestroyUnSelect(Structure structure)
        {
            _currentStructure.OnDestroyEvent -= HandleDestroyUnSelect;
            _infoPanel.Close();
            _infoPanel.Dispose();
        }

        private void HandleMove(Vector2 direction)
        {// UpgradeSelect
            if (_canUpgradeStructure)
            {
                _currentSelectIndex = Mathf.Clamp((_currentSelectIndex + (int)direction.x) % _structureData.upgradeList.Length, 0, _structureData.upgradeList.Length - 1);

                SelectUpgradeIndex(_currentSelectIndex);
            }

        }

        private void SelectUpgradeIndex(int index)
        {
            _selector.UpgradePanel.SelectSlot(index);
        }

        private void HandleCancel()
        {
            _currentStructure.OnDestroyEvent -= HandleDestroyUnSelect;
            _stateMachine.ChangeState(SelectorStateEnum.Stay);
        }

        private void HandleDestroyStructure()
        {
            _infoPanel.Close();
            _infoPanel.Dispose();
            _currentStructure.OnDestroyEvent -= HandleDestroyUnSelect;
            _currentStructure.DestroyStructure();
            _stateMachine.ChangeState(SelectorStateEnum.Stay);
        }

        private void HandleUpgrade()
        {
            if (_canUpgradeStructure)
            {

                _selector.BuildController.BuildStructure(_structureData.upgradeList[_currentSelectIndex], _selector.transform.position);
                HandleDestroyStructure();
            }
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