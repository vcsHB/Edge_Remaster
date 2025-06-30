using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace BuildSystem.SelectorManage
{

    public class SelectionSlot : MonoBehaviour
    {
        public SerializeDictionary<Vector2Int, SelectionSlot> _groupedSlots;


        public UnityEvent OnSelectedEvent;
        [SerializeField] private Image _selectionImage;
        public void ResetTransition()
        {
            _groupedSlots?.Clear();
        }

        public void SetTransition(Vector2Int origin, SelectionSlot target)
        {
            _groupedSlots[-origin] = target;

        }


        public virtual void OnDeselect(Vector2Int direction)
        {
            _selectionImage.enabled = false;
        }

        public virtual void OnSelect(Vector2Int direction)
        {
            _selectionImage.enabled = true;

        }
        public bool GetDirectionSlot(Vector2Int direction, out SelectionSlot slot)
        {
            if (_groupedSlots.TryGetValue(direction, out slot))
            {
                return true;
            }
            return false;
        }



    }
}