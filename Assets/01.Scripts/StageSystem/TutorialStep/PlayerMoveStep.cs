using ObjectManage;
using UnityEngine;
namespace StageSystem.TutorialManage
{

    public class PlayerMoveStep : TutorialStep
    {
        [SerializeField] private Transform _movePointNearbyPositionTrm;
        [SerializeField] private float _detectRadius;
        [SerializeField] private LayerMask _movePointLayer;
        private MovePoint _targetMovePoint;
        private void Awake()
        {
            Collider2D collider = Physics2D.OverlapCircle(_movePointNearbyPositionTrm.position, _detectRadius, _movePointLayer);
            if (collider == null)
            {
                Debug.LogError("There is not detected MovePoint");
                return;
            }
            _targetMovePoint = collider.GetComponent<MovePoint>();
            _targetMovePoint.OnEnterEvent.AddListener(HandleMoveEnter);
            _movePointNearbyPositionTrm.position = collider.transform.position;

        }

        private void HandleMoveEnter()
        {
            _targetMovePoint.OnEnterEvent.RemoveListener(HandleMoveEnter);
            Exit();
        }

        public override void Enter()
        {
            base.Enter();
            _movePointNearbyPositionTrm.gameObject.SetActive(true);

        }

        public override void Exit()
        {
            base.Exit();
            _movePointNearbyPositionTrm.gameObject.SetActive(false);
        }

    }
}