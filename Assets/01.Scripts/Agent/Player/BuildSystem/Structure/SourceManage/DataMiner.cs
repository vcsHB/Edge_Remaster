using BuildSystem.Structures;
using UnityEngine;
namespace BuildSystem.ResourceManage
{

    public class DataMiner : EnergyRequireStructure
    {
        [SerializeField] private float _mineCooltime = 10.2f;
        [SerializeField] private ResourceType _mineDataType;
        [SerializeField] private int _extractAmount = 10;
        private float _currentCooltime = 0f;
        [SerializeField] private Transform _gearTrm;
        [SerializeField] private float _gearRotationSpeed = 3f;


        protected override void Awake()
        {
            base.Awake();

        }

        private void Update()
        {
            _currentCooltime += Time.deltaTime * WorkSpeed;
            _gearTrm.localRotation = Quaternion.Euler(0f, 0f, _gearTrm.eulerAngles.z + _gearRotationSpeed * WorkSpeed);
            if (_currentCooltime >= _mineCooltime)
            {
                _currentCooltime = 0f;
                ExtractData();
            }
        }

        private void ExtractData()
        {
            ResourceManager.Instance.GainResource(_mineDataType, _extractAmount);

        }

    }
}