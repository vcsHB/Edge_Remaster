using Agents;
using Combat;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIManage.InGame
{

    public class HealthGauge : MonoBehaviour
    {
        [SerializeField] private Health _owner;
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _changeDuration = 0.1f;
        private void Awake()
        {
            _owner.OnHealthChangedValueEvent += HandleGaugeRefresh;
        }

        private void OnDestroy()
        {
            _owner.OnHealthChangedValueEvent -= HandleGaugeRefresh;
        }
        public void SetOwner(Health newOwner)
        {
            if (_owner != null)
                _owner.OnHealthChangedValueEvent -= HandleGaugeRefresh;
            _owner = newOwner;
            HandleGaugeRefresh(_owner.CurrentHealth, _owner.MaxHealth);
            _owner.OnHealthChangedValueEvent += HandleGaugeRefresh;
        }


        private void HandleGaugeRefresh(float current, float max)
        {
            float ratio = current / max;
            _fillImage.DOFillAmount(ratio, _changeDuration);
        }
    }


}