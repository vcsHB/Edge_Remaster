using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
namespace BuildSystem.Structures.Turrets
{

    public class BurstAttackVisual : MonoBehaviour
    {
        public UnityEvent OnVFXPlayEvent;
        [SerializeField] private Transform[] _vfxTrms;
        [SerializeField] private Vector2 _enableScale;
        [SerializeField] private float _enableDuration = 0.1f;
        [SerializeField] private float _disableDuration = 0.1f;
        public void Play()
        {
            OnVFXPlayEvent?.Invoke();
            for (int i = 0; i < _vfxTrms.Length; i++)
            {
                Transform vfxTrm = _vfxTrms[i];
                vfxTrm.DOScale(_enableScale, _enableDuration).OnComplete(() =>
                {
                    vfxTrm.DOScale(Vector3.zero, _disableDuration);
                });
            }
        }
    }
}