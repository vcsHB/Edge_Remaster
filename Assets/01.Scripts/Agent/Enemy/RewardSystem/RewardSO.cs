using Unity.Cinemachine;
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
        public RewardGroup[] rewards; // It need Serialized Dictionary

        public int GetCrystalAmount()
        {
            int totalCrystal = 0;
            for (int i = 0; i < rewards.Length; i++)
            {
                if (rewards[i].rewardType == RewardType.Crystal)
                {
                    totalCrystal += rewards[i].amount;
                }
            }
            return totalCrystal;
        }
    }
}