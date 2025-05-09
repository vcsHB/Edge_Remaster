using Agents.Enemies.AI.PathFinder;
using NUnit.Compatibility;
using UnityEngine;
namespace Agents.Enemies.AI.Ability
{

    public class TimeBreakAbility : AbilityLogicSO
    {
        [SerializeField] private float _newTimeSet;
        private AvoidingPathfinder _avoidPathFinder;
        private Vector2[] _pathData;
        public override void InitializeOwner(Enemy owner)
        {
            base.InitializeOwner(owner);
        }
        protected override void UseAbility()
        {
            _pathData = _avoidPathFinder.FindPath(_ownerTrm.position, _detectData.targetPos);
            if(_pathData == null) return;
            

        }
    }
}