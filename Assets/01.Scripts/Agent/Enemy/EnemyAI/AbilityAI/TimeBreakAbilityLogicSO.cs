using System;
using Agents.Enemies.AI.PathFinder;
using UnityEngine;
namespace Agents.Enemies.AI.Ability
{
    [CreateAssetMenu(menuName = "SO/EnemyAI/Avility/TimeBreak")]
    public class TimeBreakAbilityLogicSO : AbilityLogicSO
    {
        [SerializeField] private float _newTimeSet;
        private AvoidingPathfinder _avoidPathFinder;
        private TimeBreakerMovement _timeBreakerMover;
        private Vector2[] _pathData;
        public override void InitializeOwner(Enemy owner)
        {
            base.InitializeOwner(owner);
            _timeBreakerMover = owner.GetCompo<TimeBreakerMovement>();
        }

        protected override void UseAbility(Action OnCompleteEvent = null)
        {
            _pathData = _avoidPathFinder.FindPath(_ownerTrm.position, _detectData.targetPos);
            if (_pathData == null) return;

            //TimeManager.AddTimeScaleRecord()
            //_mover.   
            _timeBreakerMover.MoveToPoints(_pathData, OnCompleteEvent);
        }
    }
}