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
        private float _currentCooltime;           
        protected Vector2 _targetDirection;
        protected bool _isAimAligned;


        protected virtual void FixedUpdate()
        {
            if (!_battery.IsEnough(_requireEnergy)) return;

            _currentCooltime += Time.fixedDeltaTime * WorkSpeed;
            _target = _targetDetector.DetectClosestTarget();
            if (_target == null) return;

            _targetDirection = _target.transform.position - transform.position;
            _isAimAligned = _mainHead.SetDirection(_targetDirection);
            if (_isAimAligned && _fireTerm < _currentCooltime)
            {
                _currentCooltime = 0f;
                _battery.TryUseEnergy(_requireEnergy);
                Fire();
            }

        }

        public override void HandleStructureSelected()
        {
            base.HandleStructureSelected();
            _targetDetector.OpenRangeVisual();
        }

        public override void HandleStructureUnselected()
        {
            base.HandleStructureUnselected();
            _targetDetector.CloseRangeVisual();
        }

        public abstract void Fire();
    }
}