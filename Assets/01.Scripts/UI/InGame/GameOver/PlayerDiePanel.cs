using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
namespace UIManage.InGame
{

    public class PlayerDiePanel : MonoBehaviour, IWindowPanel
    {
        public UnityEvent OnPanelOpenEvent;
        [SerializeField] private float _eventDelay = 1f;
        [SerializeField] private float _fadeDuration = 0.2f;
        [SerializeField] private bool _useUnscaledTime = true;

        [Header("MainPanel Settings")]
        [SerializeField] private RectTransform _mainPanelTrm;
        [SerializeField] private float _mainPanelWidth = 300f;
        [SerializeField] private float _mainPanelSizingDuration = 1.5f;
        [Header("SideBar Settings")]
        [SerializeField] private RectTransform _leftBarTrm;
        [SerializeField] private RectTransform _rightBarTrm;
        [SerializeField] private float _sideBarEnablePosition;
        [SerializeField] private float _sideBarDisablePosition;
        [SerializeField] private float _sideBarMoveDuration = 1.2f;
        private CanvasGroup _canvasGroup;


        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        [ContextMenu("DebugClose")]
        public void Close()
        {

            _mainPanelTrm.DOSizeDelta(new Vector2(0f, _mainPanelTrm.sizeDelta.y), _mainPanelSizingDuration).SetUpdate(_useUnscaledTime);
            _leftBarTrm.anchoredPosition = new Vector2(-_sideBarEnablePosition, 0f);
            _rightBarTrm.anchoredPosition = new Vector2(_sideBarEnablePosition, 0f);
            _leftBarTrm.DOAnchorPosX(-_sideBarDisablePosition, _sideBarMoveDuration).SetUpdate(_useUnscaledTime);
            _rightBarTrm.DOAnchorPosX(_sideBarDisablePosition, _sideBarMoveDuration).SetUpdate(_useUnscaledTime);
            _canvasGroup.DOFade(0f, _fadeDuration + 1f).SetUpdate(_useUnscaledTime);

        }

        [ContextMenu("DebugOpen")]
        public void Open()
        {
            _canvasGroup.DOFade(1f, _fadeDuration).SetUpdate(_useUnscaledTime);
            _mainPanelTrm.DOSizeDelta(new Vector2(_mainPanelWidth, _mainPanelTrm.sizeDelta.y), _mainPanelSizingDuration)
            .SetUpdate(_useUnscaledTime).OnComplete(() => DOVirtual.DelayedCall(_eventDelay, () =>
            {
                OnPanelOpenEvent?.Invoke();
            }));
            _leftBarTrm.anchoredPosition = new Vector2(-_sideBarDisablePosition, 0f);
            _rightBarTrm.anchoredPosition = new Vector2(_sideBarDisablePosition, 0f);
            _leftBarTrm.DOAnchorPosX(-_sideBarEnablePosition, _sideBarMoveDuration).SetUpdate(_useUnscaledTime);
            _rightBarTrm.DOAnchorPosX(_sideBarEnablePosition, _sideBarMoveDuration).SetUpdate(_useUnscaledTime);
        }
    }
}