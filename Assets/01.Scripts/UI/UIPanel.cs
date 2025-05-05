using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{

    public class UIPanel : MonoBehaviour, IWindowPanel
    {
        public UnityEvent OnOpenEvent;
        public UnityEvent OnCloseEvent;
        [SerializeField] protected bool _useUnscaledTime;
        [SerializeField] protected float _duration;
        protected CanvasGroup _canvasGroup;
        [SerializeField] protected bool _isActive;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }


        public virtual void Close()
        {
            SetActiveCanvasGroup(false);
            OnCloseEvent?.Invoke();
        }

        public virtual void Open()
        {
            SetActiveCanvasGroup(true);
            OnOpenEvent?.Invoke();
        }

        public void SetActiveCanvasGroup(bool value)
        {
            Tween tween = _canvasGroup.DOFade(value ? 1f : 0f, _duration)
                .SetUpdate(_useUnscaledTime).OnComplete(() => _isActive = value);

            SetInteractable(value);
        }


        public void SetCanvasActiveImmediately(bool value)
        {
            _canvasGroup.alpha = value ? 1f : 0f;
            SetInteractable(value);
        }

        protected void SetInteractable(bool value)
        {
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }
    }

}