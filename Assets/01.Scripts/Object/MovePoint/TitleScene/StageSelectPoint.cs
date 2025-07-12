using System;
using StageSystem;
using UIManage;
using UIManage.TitleScene;
using UnityEngine;
namespace ObjectManage
{

    public class StageSelectPoint : MonoBehaviour
    {
        [SerializeField] private StageDataSO _stageData;
        [SerializeField] private StageDetailPanel _stageDetailPanel;
        private MovePoint _movePoint;

        private void Awake()
        {
            _movePoint = GetComponent<MovePoint>();
            _movePoint.OnEnterEvent.AddListener(HandlePointEnter);
            _movePoint.OnExitEvent.AddListener(HandlePointExit);
        }

        private void HandlePointEnter()
        {
            _stageDetailPanel.Open();
            _stageDetailPanel.SetStageData(_stageData);
        }

        private void HandlePointExit()
        {
            _stageDetailPanel.Close();

        }
    }
}