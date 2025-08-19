using System;
using System.Collections.Generic;
using BuildSystem.Structures;
using Combat.WaveSystem;
using Core.EventSystem;
using UnityEngine;
namespace BuildSystem
{
    public class BuildData : GameEvent
    {
        public StructureDataSO data;
        public Vector2 position;
    } // BuildEventChannelData

    public class DestroyData : GameEvent
    {
        public StructureDataSO data;
    } // BuildEventChannelData

    public class UpgradeData : GameEvent
    {
        public StructureDataSO previousStructure;
        public StructureDataSO newStructure;
    }


    public class BuildController : MonoBehaviour
    {
        [field: SerializeField] public GameEventChannelSO BuildEventChannel { get; private set; }
        public event Action<StructureDataSO> OnBuildEvent;
        public event Action<StructureDataSO> OnDestroyEvent;
        [SerializeField] private List<Structure> _structures = new();
        [SerializeField] WaveManager _waveManager;
        private BuildData _buildData = new();
        private DestroyData _destroyData = new();
        private UpgradeData _upgradeData = new();

        private void Awake()
        {
            // Load

        }

        public void UpgradeSturcture(StructureDataSO previousStructure, StructureDataSO newStructure)
        {
            _upgradeData.previousStructure = previousStructure;
            _upgradeData.newStructure = newStructure;
            BuildEventChannel.RaiseEvent(_upgradeData);
        }

        public void BuildStructure(StructureDataSO data, Vector2 position)
        {
            Structure structure = Instantiate(data.structurePrefab, position, Quaternion.identity);
            structure.OnDestroyEvent += HandleStructureDestroy;
            _structures.Add(structure);
            _waveManager.OnWaveStartEvent.AddListener(structure.HandleWaveStart);
            OnBuildEvent?.Invoke(data);
            _buildData.data = data;
            _buildData.position = position;
            BuildEventChannel.RaiseEvent(_buildData);
        }

        private void HandleStructureDestroy(Structure structure)
        {
            if (structure is Pump pump)
            {
                _waveManager.OnWaveStartEvent.RemoveListener(pump.PumpEnergy);
            }
            _structures.Remove(structure);
            _destroyData.data = structure.DataSO;
            OnDestroyEvent?.Invoke(structure.DataSO);
            BuildEventChannel.RaiseEvent(_destroyData);
        }
    }
}