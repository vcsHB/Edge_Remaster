using BuildSystem.Structures;
using UnityEngine;
namespace BuildSystem.ResourceManage
{

    public class EnergyVisualRenderer : MonoBehaviour
    {
        private Color _defaultColor;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _disableColor = Color.black;
        [SerializeField, Range(0f, 1f)] private float _opacity;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultColor = _spriteRenderer.color;
        }

        public void SetVisualEnable(bool value)
        {
            if (value)
                _spriteRenderer.color = _defaultColor;
            else
                _spriteRenderer.color = Color.Lerp(_defaultColor, _disableColor, _opacity);
        }




    }
}