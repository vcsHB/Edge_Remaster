using System;
using UnityEngine;
namespace StageSystem
{

    public class StageManager : MonoBehaviour
    {
        public event Action OnWaveStartEvent;
        [SerializeField] private StageDataGroupSO _stageGroupData;
        private StageDataSO _currentStageData;
        private StageLevel _currentLevel;

        public StageDataSO InitializeStage(int enterStageId)
        {
            _currentStageData = _stageGroupData.GetData(enterStageId);
            if (_currentStageData == null)
            {
                Debug.LogError($"Stage data is null. ID : {enterStageId}");
                return null;
            }

            _currentLevel = Instantiate(_currentStageData.stageLevelPrefab, Vector2.zero, Quaternion.identity);
            _currentLevel.OnMapInitOverEvent += HandleMapInitOver;
            return _currentStageData;

        }

        private void HandleMapInitOver()
        {
            OnWaveStartEvent?.Invoke();
        }
    }
}