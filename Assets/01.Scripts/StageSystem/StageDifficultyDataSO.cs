using Unity.VisualScripting;
using UnityEngine;
namespace StageSystem
{
    [CreateAssetMenu(menuName = "SO/Stage/Diffuculty")]
    public class StageDifficultyDataSO : ScriptableObject
    {
        public string difficultyName;
        public Color difficultyColor;
        [Range(0f, 1f)] public float difficultyLevel;
    }
}