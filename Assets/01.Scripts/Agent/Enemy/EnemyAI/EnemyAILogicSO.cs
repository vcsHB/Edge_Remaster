using System;
using UnityEngine;
namespace Agents.Enemies.AI
{

    // Must be implemented in child objects
    public abstract class EnemyAILogicSO : ScriptableObject
    {
        public event Action<bool> OnDetectTarget;
        
        [SerializeField] protected float _detectRadius;
        [SerializeField] protected LayerMask _targetLayer;
        protected Transform _targetTrm;
        protected Enemy _owner;
        protected Transform _ownerTrm;
        public void InitializeOwner(Enemy enemy)
        {
            _owner = enemy;
            _ownerTrm = _owner.transform;
        }
        public abstract Transform GetTarget();

        protected abstract void DetectTarget();

        public EnemyAILogicSO Clone() => Instantiate(this);
    }
}