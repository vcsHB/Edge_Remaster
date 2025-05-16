using System;
using UnityEngine;
namespace Agents.Enemies.AI
{
    public struct AbilityData
    { // Duration? Damage?
        Action OnAbilityCompleteEvent;
    }
    public abstract class AbilityLogicSO : ScriptableObject
    {
        public Action OnUseAbilityEvent;
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
        public virtual void HandleUseAbility(Action OnCompleteEvent = null)
        {
            UseAbility();
            OnUseAbilityEvent?.Invoke();
        }

        protected abstract void UseAbility(Action OnCompleteEvent = null);


        public AbilityLogicSO Clone() => Instantiate(this);
    }
}