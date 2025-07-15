using DG.Tweening;
using UnityEngine;
namespace UIManage.InGame
{

    public class GameEnterPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private Transform _miniSquareTrm;
        [SerializeField] private float _targetScale = 4f;
        [SerializeField] private float _startDelay;
        [SerializeField] private float _scaleUpDuration;
        [SerializeField] private bool _startOnAwake = true;
        private CanvasGroup _canvasGroup;



        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_startOnAwake)
                Close();
        }

        public void Close()
        {
            Invoke(nameof(StartScaleUp), _startDelay);

        }

        public void Open()
        {

        }

        private void StartScaleUp()
        {
            _miniSquareTrm.DOScale(_targetScale, _scaleUpDuration).OnComplete(() =>
            {
                _canvasGroup.alpha = 0f;
            });
        }

    }
}