using DG.Tweening;
using UIManage;
using UnityEngine;
using UnityEngine.UI;
namespace TitleScene
{

    public class AnchorPanel : UIPanel
    {
        [SerializeField] private Image _chainImage;
        private Material _chainMaterial;
        private readonly int _chainSpeedHash = Shader.PropertyToID("_Speed");
        [SerializeField] private float _chainSpeed = 5f;
        [SerializeField] float _animationDelay = 0.3f;
        [SerializeField] private float _anchorDownPosition;
        [SerializeField] private float _anchorDownDuration;
        private RectTransform _anchorTrm;

        protected override void Awake()
        {
            base.Awake();
            _chainMaterial = _chainImage.material;
            _anchorTrm = transform as RectTransform;


        }

        [ContextMenu("DebugOPen")]
        public override void Open()
        {
            base.Open();
            DOVirtual.DelayedCall(_animationDelay, () =>
            {
                transform.DOScale(1f, _anchorDownDuration * 0.7f);
                _anchorTrm.DOAnchorPosY(_anchorDownPosition, _anchorDownDuration).OnComplete(() =>
                {
                    _chainMaterial.SetFloat(_chainSpeedHash, _chainSpeed);
                    DOVirtual.DelayedCall(_animationDelay, () =>
                    {
                        Close();
                    });
                });
            });
        }
    }
}