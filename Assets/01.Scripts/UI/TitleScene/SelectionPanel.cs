using DG.Tweening;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
namespace UIManage.TitleScene
{
    public class SelectionPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private float _selectWidth = 17f;
        [SerializeField] private float _unselectWidth = 3f;
        [SerializeField] private float _panelTweenDuration = 0.4f;
        [SerializeField] private RectTransform _iconTrm;
        [SerializeField] private float _iconDefaultPos = 0.5f;
        [SerializeField] private float _iconSelectedPos;
        [SerializeField] private float _iconTweenDuration = 0.2f;
        private RectTransform _rectTrm;

        private void Awake()
        {
            _rectTrm = transform as RectTransform;
        }
        
        public void Close()
        {
            _rectTrm.DOSizeDelta(new Vector2(_unselectWidth, _rectTrm.sizeDelta.y), _panelTweenDuration);
            _iconTrm.DOAnchorPosX(_iconDefaultPos, _iconTweenDuration);
        }

        public void Open()
        {
            _rectTrm.DOSizeDelta(new Vector2(_selectWidth, _rectTrm.sizeDelta.y), _panelTweenDuration);
            _iconTrm.DOAnchorPosX(_iconSelectedPos, _iconTweenDuration);
        }
    }
}