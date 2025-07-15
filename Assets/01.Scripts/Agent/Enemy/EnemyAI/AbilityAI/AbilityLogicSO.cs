using System;
using UnityEngine;

namespace Agents.Enemies.AI
{
    public enum AbilityRank
    {
        LowRisk,
        NormalRisk,
        HyperRisk,
        FetalRisk
    }
    [System.Serializable]
    public class AilityDisplayData
    {
        public AbilityRank AbilityRank;
        public string abilityName;
        public string abilityDescription;
    }
    public abstract class AbilityLogicSO : ScriptableObject
    {
        public event Action OnUseAbilityEvent;
        public event Action OnAbilityCompleteEvent;

        public float useCooltime;
        public AilityDisplayData displayData;


        protected DetectData _detectData;

        protected Enemy _owner;
        protected Transform _ownerTrm;
        protected EnemyMovement _mover;
        protected EnemyRenderer _renderer;

        public virtual void InitializeOwner(Enemy owner)
        {
            _owner = owner;
            _ownerTrm = _owner.transform;
            _mover = _owner.GetCompo<EnemyMovement>();
            _renderer = _owner.GetCompo<EnemyRenderer>();
        }


        public void HandleDetect(DetectData detectData)
        {
            _detectData = detectData;
        }
        public virtual bool HandleUseAbility(Action OnCompleteEvent = null)
        {
            if (!_detectData.isTargeted) return false;
            UseAbility();
            OnAbilityCompleteEvent += OnCompleteEvent;
            OnUseAbilityEvent?.Invoke();
            return true;
        }

        protected abstract void UseAbility();
        protected void InvokeAbilityComplete()
        {
            OnAbilityCompleteEvent?.Invoke();
        }


        public AbilityLogicSO Clone() => Instantiate(this);
    }
}