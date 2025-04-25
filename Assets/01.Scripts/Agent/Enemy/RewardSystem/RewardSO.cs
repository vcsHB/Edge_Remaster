using UnityEngine;
namespace RewardSystem
{
    [System.Serializable]
    public class RewardGroup
    {
        public RewardType rewardType;
        public int amount;
    }
    [CreateAssetMenu(menuName = "SO/RewardSO")]
    public class RewardSO : ScriptableObject
    {
        public RewardGroup[] rewards;
    }
}