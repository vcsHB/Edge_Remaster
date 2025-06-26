using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
namespace BuildSystem.Structures.Turrets
{

    public abstract class TurretStructure : Structure
    {
        [SerializeField] private TurretHead _mainHead;
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private Collider2D _target;
        [SerializeField] private float _fireTerm = 0.2f;
        protected float _fireLimitTime;
        protected Vector2 _targetDirection;
        protected bool _isAimAligned;


        protected virtual void FixedUpdate()
        {
            _target = _targetDetector.DetectClosestTarget();
            if (_target == null) return;

            _targetDirection = _target.transform.position - transform.position;
            _isAimAligned = _mainHead.SetDirection(_targetDirection);
            if (_isAimAligned && _fireLimitTime < Time.time)
            {
                _fireLimitTime = Time.time + _fireTerm;
                Fire();
            }

        }

        public abstract void Fire();
    }
}