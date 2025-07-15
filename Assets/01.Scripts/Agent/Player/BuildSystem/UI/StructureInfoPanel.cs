using BuildSystem.Structures;
using TMPro;
using UIManage;
using UIManage.InGame;
using UnityEngine;
namespace BuildSystem.UIManage
{

    public class StructureInfoPanel : UIPanel
    {
        private Structure _owner;
        [SerializeField] private TextMeshProUGUI _structureNameText;
        [SerializeField] private HealthGauge _healthGauge;
        public void SetStructure(Structure structure)
        {
            Open();
            _structureNameText.text = structure.DataSO.structureName;
            _owner = structure;
            _healthGauge.SetOwner(structure.HealthCompo);

        }

        public void Dispose()
        {
            _healthGauge.DisposeOwner();
        }

        
    }
}