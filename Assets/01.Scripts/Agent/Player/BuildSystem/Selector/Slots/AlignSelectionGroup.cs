using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;
namespace BuildSystem.SelectorManage
{
    public enum AlignDirection
    {
        Left, Right, Up, Down
    }
    public class AlignSelectionGroup : SelectionGroup
    {
        [SerializeField] private AlignDirection _alignType;
        [SerializeField] private float _foldDuration = 0.5f;

        [SerializeField] private Transform _alignElementParent;
        [SerializeField] private float _spacing = 30f;
        private HorizontalOrVerticalLayoutGroup _layoutGroup;
        private CanvasGroup _canvasGroup;
        private bool _isActive;

        private void Awake()
        {
            _canvasGroup = _alignElementParent.GetComponent<CanvasGroup>();
            _layoutGroup = _alignElementParent.GetComponent<HorizontalOrVerticalLayoutGroup>();

        }

        protected override void OnValidate()
        {
            base.OnValidate();
            Align();
        }

        public override void OnDeselect(Vector2Int direction)
        {
            base.OnDeselect(direction);
            if (_groupedSlots.SerializedKeys[0] != direction)
            {
                _canvasGroup.DOFade(0f, _foldDuration * 0.5f);
                StartCoroutine(FoldCoroutine(false));
                _isActive = false;
            }

        }

        public override void OnSelect(Vector2Int direction)
        {
            base.OnSelect(direction);
            if (_isActive) return;
            _canvasGroup.DOFade(1f, _foldDuration * 0.5f);
            StartCoroutine(FoldCoroutine(true));
            _isActive = true;

        }

        private IEnumerator FoldCoroutine(bool value)
        {
            float start = value ? -200f : 30f;
            float end = value ? 30f : -200f;

            float currentTime = 0f;
            while (currentTime < _foldDuration)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / _foldDuration;
                _layoutGroup.spacing = Mathf.Lerp(start, end, ratio);
                yield return null;
            }
            _layoutGroup.spacing = end;
        }


        private void Align()
        {
            Vector2Int direction = ToVector2Int(_alignType);
            SelectionSlot prevTarget = this;
            int index = 0;
            foreach (Transform item in _alignElementParent)
            {
                if (item.TryGetComponent(out SelectionSlot slot))
                {
                    if (index != 0)
                    {
                        prevTarget.SetTransition(-direction, slot);
                    }
                    else
                    {
                        _groupedSlots[direction] = slot;
                    }
                    slot.SetTransition(direction, prevTarget);
                    prevTarget = slot;
                    index++;
                }
            }
        }

        public static Vector2Int ToVector2Int(AlignDirection direction)
        {
            return direction switch
            {
                AlignDirection.Left => Vector2Int.left,
                AlignDirection.Right => Vector2Int.right,
                AlignDirection.Up => Vector2Int.up,
                AlignDirection.Down => Vector2Int.down,
                _ => Vector2Int.zero
            };
        }
    }
}