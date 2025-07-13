using UnityEngine;
namespace StageSystem
{

    public class StageManager : MonoBehaviour
    {
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
            return _currentStageData;

        }
    }
}