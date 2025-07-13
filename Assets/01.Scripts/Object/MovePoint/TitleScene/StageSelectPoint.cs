using StageSystem;
using UIManage.TitleScene;
using UnityEngine;

namespace ObjectManage
{

    public class StageSelectPoint : MonoBehaviour
    {
        [SerializeField] private StageDataSO _stageData;
        public StageDataSO StageData => _stageData;
        [SerializeField] private PollutionChainObject[] _chains;
        [SerializeField] private StageDetailPanel _stageDetailPanel;
        [SerializeField] private int _requireDepthLevel;
        private MovePoint _movePoint;
        private bool _isStageEnable;
        private bool _depthLevelEnough;

        private void Awake()
        {
            _movePoint = GetComponent<MovePoint>();
            _movePoint.OnEnterEvent.AddListener(HandlePointEnter);
            _movePoint.OnExitEvent.AddListener(HandlePointExit);
        }

        private void HandlePointEnter()
        {
            if (_isStageEnable)
            {
                _stageDetailPanel.Open();
                _stageDetailPanel.SetStageData(_stageData);
            }
        }

        private void HandlePointExit()
        {
            if (_isStageEnable)
                _stageDetailPanel.Close();

        }
        public void SetStageDepthCondition(int currentDepthLevel)
        {
            _depthLevelEnough = currentDepthLevel >= _requireDepthLevel;
        }

        public void SetStageEnable(bool value)
        {
            _isStageEnable = value;
        }

        public void SetStageClear(bool value)
        {
            for (int i = 0; i < _chains.Length; i++)
            {
                _chains[i].SetEnable(!value);
            }
        }
    }
}