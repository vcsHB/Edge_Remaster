using StatSystem;
using UnityEngine;
namespace Agents
{
    public class AgentStat : MonoBehaviour, IAgentComponent
    {
        [field: SerializeField] public StatusSO Status { get; protected set; }


        private void Awake()
        {
            Status = Instantiate(Status);
        }
        public void AfterInit() { }

        public void Dispose() { }

        public void Initialize(Agent agent)
        {

        }
        public void AddModifier(StatType type, float value)
        {
            Status.GetStat(type).AddModifier(value);
        }
        public void RemoveModifier(StatType type, float value)
        {
            Status.GetStat(type).RemoveModifier(value);
        }

        public Stat GetStat(StatType statType)
        {
            return Status.GetStat(statType);
        }

        public float GetBaseValue(StatType statType)
            => GetStat(statType).BaseValue;



        // public void AddModifier(StatType statType, object key, float value)
        //     => GetStat(statType).AddBuffDebuff(key, value);


        // public void RemoveModifier(StatType statType, object key)
        //     => GetStat(statType).RemovedBuffDebuff(key);
    }
}