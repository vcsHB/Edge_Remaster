using System;
using System.Collections.Generic;
using BuildSystem.Structures;
using Combat.WaveSystem;
using UnityEngine;
namespace BuildSystem
{

    public class BuildController : MonoBehaviour
    {
        [SerializeField] private List<Structure> _structures = new();
        [SerializeField] WaveManager _waveManager;

        private void Awake()
        {
            // Load

        }

        public void BuildStructure(StructureDataSO data, Vector2 position)
        {
            Structure structure = Instantiate(data.structurePrefab, position, Quaternion.identity);
            structure.OnDestroyEvent += HandleStructureDestroy;
            _structures.Add(structure);
                _waveManager.OnWaveStartEvent.AddListener(structure.HandleWaveStart);
        }

        private void HandleStructureDestroy(Structure structure)
        {
            if (structure is Pump pump)
            {
                _waveManager.OnWaveStartEvent.RemoveListener(pump.PumpEnergy);
            }
            _structures.Remove(structure);
        }
    }
}