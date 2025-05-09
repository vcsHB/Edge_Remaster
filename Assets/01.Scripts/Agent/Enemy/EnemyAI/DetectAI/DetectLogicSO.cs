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
    public abstract class DetectLogicSO : ScriptableObject
    {
        [SerializeField] protected LayerMask _whatIsTarget;

        public event Action<DetectData> OnDetectEvent;
        protected Transform _ownerTrm;
        public virtual void InitializeOwner(Transform transform)
        {
            _ownerTrm = transform;
        }
        public abstract DetectData DetectTarget();

        public DetectLogicSO Clone() => Instantiate(this);
        protected void InvokeDetectEvent(DetectData data)
        {
            OnDetectEvent?.Invoke(data);
        }
    }
}