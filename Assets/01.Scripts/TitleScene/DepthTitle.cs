using Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
namespace TitleScene
{

    public class DepthTitle : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration;
        [SerializeField] private float _enableDuration;
        [SerializeField] private Color _disableColor;
        [SerializeField] private Color _enableColor;
        [SerializeField] private float _timeScale;

        private TextMeshPro _titleText;

        private void Awake()
        {
            _titleText = GetComponent<TextMeshPro>();
            _titleText.color = _disableColor;
        }
        public void StartOpen()
        {
            TimeManager.AddTimeScaleRecord(_timeScale);
            Sequence seq = DOTween.Sequence();
            seq.Append(_titleText.DOColor(_enableColor, _fadeDuration));
            seq.AppendInterval(_enableDuration);
            seq.Append(_titleText.DOColor(_disableColor, _fadeDuration));
            seq.SetUpdate(true);
            seq.OnComplete(TimeManager.RemoveTimeScaleRecord);

        }
    }
}