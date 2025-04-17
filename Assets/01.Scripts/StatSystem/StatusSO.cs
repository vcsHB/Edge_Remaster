using System.Collections.Generic;
using UnityEngine;
namespace StatSystem
{
    public enum StatType
    {
        Health = 0,
        AttackDamage,
        MoveSpeed,
        Defense,
        AttackSpeed,
        EdgeSlideSpeed,
        EdgeMoveCooltime,
        NoLimitDuration,
        FeverFillMultiple,
        ScoreBonus
    }
    [CreateAssetMenu(menuName = "SO/Status/Status")]
    public class StatusSO : ScriptableObject
    {
        public Dictionary<StatType, Stat> statDictionary = new Dictionary<StatType, Stat>();

        public Stat health;
        public Stat attackDamage;
        public Stat moveSpeed;
        public Stat defense;
        public Stat attackSpeed;

        public void AddModifier(StatType type, float value)
        {
            GetStat(type).AddModifier(value);
        }
        public void RemoveModifier(StatType type, float value)
        {
            GetStat(type).RemoveModifier(value);
        }

        public Stat GetStat(StatType statType)
        {
            if (statDictionary.TryGetValue(statType, out Stat stat))
            {
                return stat;
            }
            Debug.LogError($"Unsupported agent Status. Type:{statType}");
            return null;
        }

        protected virtual void OnEnable()
        {
            statDictionary.Add(StatType.Health, health);
            statDictionary.Add(StatType.AttackDamage, attackDamage);
            statDictionary.Add(StatType.MoveSpeed, moveSpeed);
            statDictionary.Add(StatType.Defense, defense);
            statDictionary.Add(StatType.AttackSpeed, attackSpeed);
        }
    }
}