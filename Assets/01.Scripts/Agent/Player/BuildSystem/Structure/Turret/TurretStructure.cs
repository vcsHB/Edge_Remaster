using UnityEngine;
namespace BuildSystem.Structures.Turrets
{

    public abstract class TurretStructure : EnergyRequireStructure
    {
        [Header("Targeting Setting")]
        [SerializeField] protected TurretHead _mainHead;
        [SerializeField] protected TargetDetector _targetDetector;
        [SerializeField] protected Collider2D _target;
        [SerializeField] protected float _fireTerm = 0.2f;

        protected float _fireLimitTime;
        protected Vector2 _targetDirection;
        protected bool _isAimAligned;


        protected virtual void FixedUpdate()
        {
            if (!_battery.IsEnough(_requireEnergy)) return;

            _target = _targetDetector.DetectClosestTarget();
            if (_target == null) return;

            _targetDirection = _target.transform.position - transform.position;
            _isAimAligned = _mainHead.SetDirection(_targetDirection);
            if (_isAimAligned && _fireLimitTime < Time.time)
            {
                _fireLimitTime = Time.time + _fireTerm;
                _battery.TryUseEnergy(_requireEnergy);
                Fire();
            }

        }

        public abstract void Fire();
    }
}