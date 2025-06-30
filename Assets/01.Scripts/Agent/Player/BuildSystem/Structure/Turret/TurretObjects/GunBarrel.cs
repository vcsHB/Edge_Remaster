using DG.Tweening;
using UnityEngine;
namespace BuildSystem.Structures.Turrets
{

    public class GunBarrel : MonoBehaviour
    {
        [Header("Shake Setting")]
        [SerializeField] private Transform _mainVisual;
        [SerializeField] private float _retractionDuration = 0.1f;
        [SerializeField] private float _advanceDuration = 0.2f;
        [SerializeField] private float _recoilDistance  = 0.3f;
        [SerializeField] private Ease _retractionEase;
        [SerializeField] private Ease _advanceEase;

        private Vector3 _originalLocalPos;

        private void Awake()
        {
            _originalLocalPos = _mainVisual.localPosition;
        }
        public void PlayRecoil()
        {
            _mainVisual.DOLocalMoveY(_originalLocalPos.y - _recoilDistance, _retractionDuration)
                .SetEase(_retractionEase)
                .OnComplete(() =>
                {
                    _mainVisual.DOLocalMoveY(_originalLocalPos.y, _advanceDuration)
                        .SetEase(_advanceEase);
                });
        }
    }
}