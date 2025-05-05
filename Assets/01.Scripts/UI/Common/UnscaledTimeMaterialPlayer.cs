using UnityEngine;
using UnityEngine.UI;
namespace UI.Common
{

    public class UnscaledTimeMaterialPlayer : MonoBehaviour
    {
        private Image _ownerImage;
        private Material _material;
        private readonly int _unscaledTimeHash = Shader.PropertyToID("_UnscaledTime");
        [SerializeField] private bool _enable;

        private void Awake()
        {
            _ownerImage = GetComponent<Image>();
            _material = _ownerImage.material;
        }

        public void SetUpdateTime(bool enable)
        {
            _enable = enable;
        }

        private void Update()
        {
            if (_enable)
                _material.SetFloat(_unscaledTimeHash, Time.unscaledTime);
        }
    }
}