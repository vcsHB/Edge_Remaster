using BuildSystem.Structures;
using UnityEngine;
using UnityEngine.UI;
namespace BuildSystem
{

    public class StructureUpgradeSlot : MonoBehaviour
    {

        [SerializeField] private Image _structureImage;
        [SerializeField] private Image _selectImage;
        public StructureDataSO Data { get; private set; }



        public void SetData(StructureDataSO data)
        {
            Data = data;
            _structureImage.sprite = data.previewImage;
        }

        public void SetEnable(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetSelect(bool value)
        {
            _selectImage.enabled = value;
        }
    }
}