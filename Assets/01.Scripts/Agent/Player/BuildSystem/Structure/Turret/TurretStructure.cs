using UnityEngine;
namespace BuildSystem.Structures
{

    public class TurretStructure : Structure
    {
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private Collider2D _target;
        private Vector2 _targetDirection;

        private void FixedUpdate()
        {
            _target = _targetDetector.DetectClosestTarget();
            _targetDirection = _target.transform.position - transform.position;
        }

        public virtual void Fire()
        {
            
        }
    }
}