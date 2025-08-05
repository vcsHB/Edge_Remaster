using CameraControllers;
using UnityEngine;
namespace TitleScene
{

    public class StageEnterChain : MonoBehaviour
    {
        [SerializeField] private float _angle;
        [SerializeField] private float _rotationDuration = 0.5f;
        private CameraRotationController _rotationController;
        private void Awake()
        {
            _rotationController = CameraManager.Instance.GetCompo<CameraRotationController>();
        }
        public void Enter()
        {
            _rotationController.Rotate(_angle, _rotationDuration);

        }
    }
}