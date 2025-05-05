using StatSystem;
using UnityEngine;
namespace Agents
{
    public class AgentStat : MonoBehaviour, IAgentComponent
    {
        [field: SerializeField] public StatusSO Status { get; protected set; }
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
            
        public void CloneStatus()
        {
            Status = Instantiate(Status);
            Status.CloneAllStatus();
        }

    }
}