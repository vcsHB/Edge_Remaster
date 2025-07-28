using BuildSystem;
using BuildSystem.Structures;
using Core.attribute;
using Core.EventSystem;
using UnityEngine;
namespace StageSystem.TutorialManage
{

    public class UpgradeStep : TutorialStep
    {

        [SerializeField] private GameEventChannelSO _buildEventChannel;
        [SerializeField] private StructureDataSO _upgradeTargetStructure;
        [SerializeField] private StructureDataSO _advancedStructure;
        [Space(10f)]
        [SerializeField] private bool _usePositionCondition;
        [SerializeField, ShowIf(nameof(_usePositionCondition))] private Transform _positionConditionTrm;

        private void Awake()
        {
            if (_buildEventChannel == null)
            {
                Debug.LogError("Build Event Channel is Not binded");
                return;
            }
            _buildEventChannel.AddListener<UpgradeData>(HandleUpgrade);
            // 스텝에 들어온 시점에. 해당 위치에 이미 설치된 것에 대한 예외 처리 필요
            // todo. 
        }

        private void HandleUpgrade(GameEvent data)
        {
            if (data is UpgradeData UpgradeData)
            {
                if (UpgradeData.previousStructure.Id == _upgradeTargetStructure.Id)
                {
                    Exit();
                }
            }
        }

        private void OnDestroy()
        {
        }

        public override void Exit()
        {
            base.Exit();
            _buildEventChannel.RemoveListener<UpgradeData>(HandleUpgrade);

        }
    }
}