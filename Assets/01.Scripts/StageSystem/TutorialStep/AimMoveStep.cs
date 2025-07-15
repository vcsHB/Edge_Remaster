using UnityEngine;
namespace StageSystem.TutorialManage
{

    public class AimMoveStep : TutorialStep
    {
        [SerializeField] private Transform _targetPointTrm;
        [SerializeField] private float _detectRadius;
        [SerializeField] private LayerMask _detectLayer;
        private bool _stepEnabled;

        public override void Enter()
        {
            base.Enter();
            _stepEnabled = true;
            _targetPointTrm.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();
            _targetPointTrm.gameObject.SetActive(false);

        }

        private void Update()
        {
            if (_stepEnabled)
                DetectTarget();

        }
        private void DetectTarget()
        {
            Collider2D collider = Physics2D.OverlapCircle(_targetPointTrm.position, _detectRadius, _detectLayer);
            if (collider == null) return;
            _stepEnabled = false;
            Exit();
        }
    }
}