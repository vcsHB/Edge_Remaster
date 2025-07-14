using BuildSystem.Structures;
using TMPro;
using UIManage;
using UnityEngine;

namespace BuildSystem.SelectorManage
{
    public class OptionSelector : UIPanel
    {
        [SerializeField] private StartSelectionGroup _root;
        [SerializeField] private SelectionSlot _currentSlot;
        public StructureDataSO StructureData { get; private set; }
        [SerializeField] private TextMeshProUGUI _buildNameText;


        private void Start()
        {
            _currentSlot = _root;
            _currentSlot.OnSelect(Vector2Int.zero);
        }

        public override void Open()
        {
            base.Open();
            _currentSlot.OnDeselect(Vector2Int.zero);
            _currentSlot = _root;
            _buildNameText.text = "";
            _currentSlot.OnSelect(Vector2Int.zero);
        }

        public void Move(Vector2 input)
        {
            if (input == Vector2.zero) return;

            Vector2Int dir = Vector2Int.RoundToInt(input);
            if (_currentSlot.GetDirectionSlot(dir, out SelectionSlot slot))
            {
                _currentSlot.OnDeselect(dir);
                _currentSlot = slot;
                _buildNameText.text = slot.selectionName;
                _currentSlot.OnSelect(dir);
                if (slot is StructureSelectionSlot buildSelection)
                {
                    StructureData = buildSelection.Data;
                }
                else
                    StructureData = null;
            }
        }
        public static Vector2Int ToDirectionInt(Vector2 input)
        {
            if (input == Vector2.zero) return Vector2Int.zero;

            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                return input.x > 0 ? Vector2Int.right : Vector2Int.left;
            }
            else
            {
                return input.y > 0 ? Vector2Int.up : Vector2Int.down;
            }
        }

    }
}