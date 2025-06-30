using System;
using BuildSystem.ResourceManage;
using BuildSystem.Structures;
using UnityEngine;
namespace BuildSystem.SelectorManage.FSM
{

    public class SelectorSelectedState : SelectorState
    {
        public SelectorSelectedState(GridSelector selector, SelectorStateMachine stateMachine) : base(selector, stateMachine)
        {
        }
        private bool _isOptionSelecting;

        public override void Enter()
        {
            base.Enter();
            _isOptionSelecting = true;
            _optionSelector.Open();
            _selector.SelectorInput.OnSelectMoveEvent += HandleMove;
            _selector.SelectorInput.OnSelectEvent += HandleSelect;
            _selector.SelectorInput.OnCancelEvent += HandleCancel;
        }

        public override void Exit()
        {
            base.Exit();
            _optionSelector.Close();
            _isOptionSelecting = false;
            _selector.SelectorInput.OnSelectMoveEvent -= HandleMove;
            _selector.SelectorInput.OnSelectEvent -= HandleSelect;
            _selector.SelectorInput.OnCancelEvent -= HandleCancel;
        }

        private void HandleCancel()
        {
            _optionSelector.Close();
            _stateMachine.ChangeState(SelectorStateEnum.Stay);

        }

        private void HandleSelect()
        {
            StructureDataSO data = _optionSelector.StructureData;
            if (data != null)
            {
                if (ResourceManager.Instance.TryUseResources(data.requireResources))
                {
                    HandleCancel();
                    _selector.BuildController.BuildStructure(data, _selector.transform.position);
                }
            }
        }

        private void HandleMove(Vector2 inputDirection)
        {
            _optionSelector.Move(inputDirection);
        }
    }
}