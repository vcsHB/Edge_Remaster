using Core.DataManage;
using UnityEngine;
namespace ObjectManage
{

    public class SelectionPointsGroup : MonoBehaviour
    {
        [SerializeField] private StageSelectPoint[] _stageSelectPoints;

        private void Start()
        {
            StageDataGroup data = DataManager.stageDataGroup;

            for (int i = 0; i < _stageSelectPoints.Length; i++)
            {
                StageData stage = data.datas[_stageSelectPoints[i].StageData.id];
                _stageSelectPoints[i].SetStageEnable(stage.isUnlocked);
                _stageSelectPoints[i].SetStageClear(stage.isCleared);
            }
        }
    }
}