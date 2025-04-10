using Agents;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIManage
{

    public class HealthGauge : MonoBehaviour
    {
        [SerializeField] private Health _owner;
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _changeDuration = 0.1f;
        private void Awake()
        {
            _owner.OnHealthValueChangedEvent += HandleGaugeRefresh;
        }

        private void OnDestroy()
        {
            _owner.OnHealthValueChangedEvent -= HandleGaugeRefresh;
        }

        private void HandleGaugeRefresh(float current, float max)
        {
            float ratio = current / max;
            _fillImage.DOFillAmount(ratio, _changeDuration);
        }
    }


}