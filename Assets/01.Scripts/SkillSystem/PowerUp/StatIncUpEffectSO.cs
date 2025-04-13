using Core;
using StatSystem;
using UnityEngine;

namespace SkillSystem
{

    [CreateAssetMenu(menuName = "SO/Items/PowerUp/Effect/StatInc")]
    public class StatIncUpEffectSO : PowerUpEffectSO
    {
        public StatType targetStat;
        public int increaseValue;

        public override void UseEffect()
        {
            PlayerManager.Instance.Player.PlayerStatus.AddModifier(targetStat, increaseValue);
        }

        public override bool CanUpgradeEffect()
        {
            return true; // 스챗 최대치를 설정했다면 그냥 true하면 안딤
        }

        private void OnValidate()
        {
            type = EffectType.StatInc;
        }
    }
}