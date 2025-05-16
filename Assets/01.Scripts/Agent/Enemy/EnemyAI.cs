using System;
using Agents.Enemies.AI;
using UnityEngine;

namespace Agents.Enemies
{

    public class EnemyAI : MonoBehaviour, IAgentComponent
    {
        [SerializeField] protected ComabtLogicSO _combatLogic;
        [SerializeField] protected DetectLogicSO _detectLogic;
        [SerializeField] protected MoveLogicSO _moveLogic;
        public MoveLogicSO MoveLogic => _moveLogic;

        // Detect Properties
        public bool IsTargeted => _detectData.isTargeted;
        public Vector2 TargetDirection => _detectData.targetDirection;
        public float DistanceToTarget => _detectData.distanceToTarget;
        public Vector2 TargetPos => _detectData.targetPos;
        protected DetectData _detectData;

        // Movemenet Properties
        public bool IsStatic => _moveData.isStatic;
        public bool IsForceMove => _moveData.isForceMove;
        public Vector2 ForceMovePos => _moveData.forceMovePos;
        public Vector2 MoveDirection => _moveData.direction;
        protected MoveData _moveData;

        protected Enemy _owner;
        public virtual void Initialize(Agent agent)
        {
            _owner = agent as Enemy;
            //_combatLogic = _combatLogic.Clone();
            _detectLogic = _detectLogic.Clone();
            _moveLogic = _moveLogic.Clone();
            _detectLogic.InitializeOwner(_owner.transform);
            _moveLogic.SetOwner(_owner);

            _detectLogic.OnDetectEvent += _moveLogic.HandleDetect;
            //_enemyMovement = agent.GetCompo<EnemyMovement>();
        }

        protected virtual void Update()
        {
            _detectData = _detectLogic.DetectTarget();
        }

        private void OnDestroy()
        {
            _detectLogic.OnDetectEvent -= _moveLogic.HandleDetect;
            Destroy(_detectLogic);
            Destroy(_moveLogic);


        }
        public void AfterInit() { }
        public void Dispose() { }

        public void ChangeDetectLogic(MoveLogicSO newMoveLogic)
        {
            // Dev After
        }

        public void StartMove()
        {
            _moveLogic.StartMove();
        }
        public void UpdateMove()
        {
            _moveLogic.UpdateMove();
        }

        public void EndMove()
        {
            _moveLogic.EndMove();
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            if (_moveLogic != null)
                _moveLogic.DrawGizmos();
        }

#endif
    }

}