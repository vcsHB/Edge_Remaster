using UnityEngine;
namespace BuildSystem.Structures.Turrets
{

    public class TurretStructure : Structure
    {
        [SerializeField] private TurretHead _mainHead;
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private Collider2D _target;
        private Vector2 _targetDirection;

        private void FixedUpdate()
        {
            _target = _targetDetector.DetectClosestTarget();
            if (_target == null) return;

            _targetDirection = _target.transform.position - transform.position;
            if (_mainHead.SetDirection(_targetDirection))
            {
                // Fire
            }
        }

        public virtual void Fire()
        {

        }
    }
}