using DG.Tweening;
using UnityEngine;
namespace UIManage
{

    public class HorizontalScaledPanel : UIPanel
    {
        [SerializeField] private float _enableHeight = 300f;
        [SerializeField] private RectTransform _panelTrm;
        
        protected override void Awake()
        {
            base.Awake();
            if (_panelTrm == null)
                _panelTrm = transform as RectTransform;
        }

        public override void Open()
        {
            base.Open();
            _panelTrm.DOSizeDelta(new Vector2(_panelTrm.sizeDelta.x, _enableHeight), _duration).SetUpdate(_useUnscaledTime);
        }

        public override void Close()
        {
            base.Close();
            _panelTrm.DOSizeDelta(new Vector2(_panelTrm.sizeDelta.x, 0f), _duration).SetUpdate(_useUnscaledTime);

        }
    }
}