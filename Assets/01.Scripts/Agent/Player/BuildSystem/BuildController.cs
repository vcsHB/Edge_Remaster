using System.Collections.Generic;
using BuildSystem.Structures;
using UnityEngine;
namespace BuildSystem
{

    public class BuildController : MonoBehaviour
    {
        [SerializeField] private List<Structure> _structures = new();

        private void Awake()
        {
            // Load

        }

        public void BuildStructure(StructureDataSO data, Vector2 position)
        {
            Structure structure = Instantiate(data.structurePrefab, position, Quaternion.identity);
            _structures.Remove(structure);
        }
    }
}