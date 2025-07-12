using UnityEngine;
namespace StageSystem
{
    [CreateAssetMenu(menuName ="SO/Stage/StageData")]
    public class StageDataSO : ScriptableObject
    {
        public int id;
        public string stageName;
        public StageDifficultyDataSO difficulty;
        public StageDetailOption[] details;
        public string stageDescription;


    }
}