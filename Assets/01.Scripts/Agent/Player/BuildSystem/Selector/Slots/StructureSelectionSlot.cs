using BuildSystem.Structures;
using UnityEngine;
using UnityEngine.UI;
namespace BuildSystem.SelectorManage
{

    public class StructureSelectionSlot : SelectionSlot
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private StructureDataSO _data;
        public StructureDataSO Data => _data;

        private void Awake()
        {
            _iconImage = transform.Find("PreviewImage").GetComponent<Image>();
            _iconImage.sprite = _data.previewImage;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {

        }
#endif

    }
}