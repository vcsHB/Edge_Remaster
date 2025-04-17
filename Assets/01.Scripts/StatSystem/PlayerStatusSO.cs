using UnityEngine;
namespace StatSystem
{
   


    [CreateAssetMenu(menuName = "SO/Status/PlayerStatus")]
    public class PlayerStatusSO : StatusSO
    {


        public Stat edgeSlideSpeed;
        public Stat edgeMoveCooltime;
        public Stat noLimitDuration;

        public Stat feverFillMultiple;
        public Stat scoreBonus;

        public void AddModifier(StatType targetStat, int increaseValue)
        {
            if(statDictionary.TryGetValue(targetStat, out Stat stat))
            {
                stat.AddModifier(increaseValue);
            }
        }

        public void RemoveModifier(StatType targetStat, int increaseValue)
        {
            if(statDictionary.TryGetValue(targetStat, out Stat stat))
            {
                stat.RemoveModifier(increaseValue);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            statDictionary.Add(StatType.EdgeSlideSpeed, edgeSlideSpeed);
            statDictionary.Add(StatType.EdgeMoveCooltime, edgeMoveCooltime);
            statDictionary.Add(StatType.NoLimitDuration, noLimitDuration);
            statDictionary.Add(StatType.FeverFillMultiple, feverFillMultiple);
            statDictionary.Add(StatType.ScoreBonus, scoreBonus);
        }
    }
}