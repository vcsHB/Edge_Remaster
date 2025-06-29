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
        public override void Enter()
        {
            base.Enter();
            _currentStructure = _selector.DetectStructure();
            _selector.SelectorInput.OnSelectMoveEvent += HandleMove;
            _selector.SelectorInput.OnUpgradeEvent += HandleUpgrade;
            _selector.SelectorInput.OnBuildDestroyEvent += HandleDestroyStructure;
        }

        private void HandleDestroyStructure()
        {
            _currentStructure.DestroyStructure();
        }

        private void HandleUpgrade()
        {
            _stateMachine.ChangeState(SelectorStateEnum.Stay);
        }

        public override void Exit()
        {
            base.Exit();
            _infoPanel.Close();
            _selector.SelectorInput.OnCancelEvent -= HandleUpgrade;
            _selector.SelectorInput.OnBuildDestroyEvent -= HandleDestroyStructure;
        }


        private void HandleMove(Vector2 inputDirection)
        {
            _stateMachine.ChangeState(SelectorStateEnum.Move);
            _mover.HandleMove(inputDirection);
        }

    }
}