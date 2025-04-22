using System;
using UnityEngine;
namespace Agents.Enemies.AI
{
    public struct DetectData
    {
        public bool isTargeted;
        public Vector2 targetPos;
        public Vector2 targetDirection;
        public float distanceToTarget;
    }
    public abstract class EnemyAILogicSO : ScriptableObject
    {
        [SerializeField] protected LayerMask _whatIsTarget;

        public event Action<DetectData> OnDetectEvent;
        protected Transform _ownerTrm;
        public void InitializeOwner(Transform transform)
        {
            _ownerTrm = transform;
        }
        public abstract DetectData DetectTarget();

        public EnemyAILogicSO Clone() => Instantiate(this);
    }
}