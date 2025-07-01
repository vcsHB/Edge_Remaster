using BuildSystem.Structures;
using UnityEngine;
namespace BuildSystem.ResourceManage
{

    public class DataMiner : EnergyRequireStructure
    {
        [SerializeField] private float _miningSpeed;
        [SerializeField] private float _mineCooltime = 0.2f;
        [SerializeField] private ResourceType _mineDataType;
        [SerializeField] private int _extractAmount = 10;


        protected override void Awake()
        {
            base.Awake();

        }

        private void ExtractData()
        {
            ResourceManager.Instance.GainResource(_mineDataType, _extractAmount);

        }

    }
}