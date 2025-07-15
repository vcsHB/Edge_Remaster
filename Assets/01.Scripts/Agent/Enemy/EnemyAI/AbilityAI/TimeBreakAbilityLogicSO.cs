using System;
using Agents.Enemies.AI.PathFinder;
using Core;
using DG.Tweening;
using UnityEngine;
namespace Agents.Enemies.AI.Ability
{
    [CreateAssetMenu(menuName = "SO/EnemyAI/Avility/TimeBreak")]
    public class TimeBreakAbilityLogicSO : AbilityLogicSO
    {
        [SerializeField] private float _newTimeSet = 0.2f;

        [Header("TimeBreak MoveAbility Settings")]
        [SerializeField] private float _moveDuration = 0.15f;
        [SerializeField] private float _moveTermDuration = 0.1f;
        [SerializeField] private Ease _movementEase;
        private AvoidingPathfinder _avoidPathFinder;
        private TimeBreakerMovement _timeBreakerMover;
        private Vector2[] _pathData;

        public override void InitializeOwner(Enemy owner)
        {
            base.InitializeOwner(owner);
            _timeBreakerMover = owner.GetCompo<TimeBreakerMovement>();
            _avoidPathFinder = owner.GetComponentInChildren<AvoidingPathfinder>();
            OnAbilityCompleteEvent += HandleAbilityComplete;
        }

        private void OnDestroy()
        {
            OnAbilityCompleteEvent -= HandleAbilityComplete;

        }

        protected override void UseAbility()
        {
            _pathData = _avoidPathFinder.FindPath(_ownerTrm.position, _detectData.targetPos);
            _avoidPathFinder.DrawDebugPathLine(_pathData);
            if (_pathData == null)
            {
                InvokeAbilityComplete();
                return;
            }
            TimeManager.AddTimeScaleRecord(_newTimeSet);
            _timeBreakerMover.MoveToPoints(
                _pathData, _moveDuration,
                _moveTermDuration, _movementEase, InvokeAbilityComplete);

        }

        private void HandleAbilityComplete()
        {
            TimeManager.RemoveTimeScaleRecord();
        }


    }
}