using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace CameraControllers
{

    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField] private CinemachineCamera _camera;
        private Dictionary<Type, ICameraControlable> _controllers = new Dictionary<Type, ICameraControlable>();
        public Transform CurrentFollowTarget => _camera.Follow;
        [SerializeField] private Transform _defaultFollowTarget;
        private CinemachinePositionComposer _positionComposer;
        private Vector3 _defaultFollowOffset;

        protected override void Awake()
        {
            base.Awake();

            _positionComposer = _camera.GetComponent<CinemachinePositionComposer>();
            if (_positionComposer != null)
            {
                Debug.Log("Test");
                _defaultFollowOffset = _positionComposer.TargetOffset;
            }
            GetComponentsInChildren<ICameraControlable>(true)
               .ToList().ForEach(controller => _controllers.Add(controller.GetType(), controller));
            foreach (ICameraControlable controller in _controllers.Values)
            {
                controller.Initialize(_camera);
            }
        }


        public T GetCompo<T>(bool isDerived = false) where T : class
        {
            if (_controllers.TryGetValue(typeof(T), out ICameraControlable compo))
            {
                return compo as T;
            }

            if (!isDerived) return default;

            Type findType = _controllers.Keys.FirstOrDefault(x => x.IsSubclassOf(typeof(T)));
            if (findType != null)
                return _controllers[findType] as T;

            return default(T);
        }

        public void SetFollow(Transform target)
        {
            _camera.Follow = target;
        }

        public void ResetFollow()
        {
            if (_defaultFollowTarget == null) return;
            _camera.Follow = _defaultFollowTarget;
        }

        public void SetFollowOffset(Vector2 newOffset)
        {
            Vector3 offset = (Vector3)newOffset;
            offset.z = -10f;
            _positionComposer.TargetOffset = offset;
        }
        public void ResetFollowOffset()
        {
            _positionComposer.TargetOffset = _defaultFollowOffset;
        }




    }
}