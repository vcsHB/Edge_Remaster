using System;
using Agents.Enemies.FSM;
using UnityEngine;
namespace Agents.Enemies.AI
{
    public struct MoveData
    {
        public bool isStatic;
        public bool isForceMove;
        public Vector2 forceMovePos;
        public Vector2 direction;

    }
    public abstract class MoveLogicSO : ScriptableObject
    {
        public event Action OnArriveEvent;
        [SerializeField] protected float _moveSpeed;

        protected Enemy _owner;
        protected Transform _ownerTrm;
        protected EnemyMovement _mover;
        protected DetectData _detectData;
        protected EnemyAI _enemyAI;

        public void HandleDetect(DetectData detectData)
        {
            _detectData = detectData;
        }

        public void Initialize(Enemy owner, EnemyAI enemyAI)
        {
            _owner = owner;
            _enemyAI = enemyAI;
            _enemyAI.DetectLogic.OnDetectEvent += HandleTargetDetect;
            _ownerTrm = _owner.transform;
            _mover = _owner.GetCompo<EnemyMovement>();

        }

        private void HandleTargetDetect(DetectData data)
        {
            _detectData = data;
        }

        public abstract void StartMove();
        public abstract void UpdateMove();
        public abstract void EndMove();


        public MoveLogicSO Clone() => Instantiate(this);
        public virtual void DrawGizmos() { }
        protected virtual void Arrive()
        {
            OnArriveEvent?.Invoke();
        }
    }
}