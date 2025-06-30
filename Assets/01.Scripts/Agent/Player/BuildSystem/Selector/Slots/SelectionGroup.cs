using UnityEngine;
namespace BuildSystem.SelectorManage
{

    public class SelectionGroup : SelectionSlot
    {
      

        public override void OnSelect(Vector2Int direction)
        {
            base.OnSelect(direction);
        }

        public override void OnDeselect(Vector2Int direction)
        {
            base.OnDeselect(direction);
        }

        protected virtual void OnValidate()
        {
            foreach (var item in _groupedSlots)
            {
                //item.Value.ResetTransition();
                item.Value.SetTransition(item.Key, this);
            }
        }

    }
}