using ObjectPooling;
using UnityEngine;

namespace ObjectManage
{

    public class GhostShadowVFXPlayer : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolingType type { get; set; }

        public GameObject ObjectPrefab => gameObject;

        [SerializeField] private float _lifeTime = 1f;
        [SerializeField] private Gradient _colorGradient;
        private SpriteRenderer _spriteRenderer;
        private bool _isActive;
        private float _currentLifeTime;


        private void Awake()
        {
            _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
        }

        public void Initialize(Vector2 position, float lifeTime, Gradient gradient, Sprite sprite)
        {
            transform.position = position;
            _lifeTime = lifeTime;
            _colorGradient = gradient;
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.color = _colorGradient.Evaluate(0f);
        }

        public void Play()
        {
            _isActive = true;
        }


        private void Update()
        {
            if (!_isActive) return;
            _currentLifeTime += Time.deltaTime;
            float ratio = _currentLifeTime / _lifeTime;
            _spriteRenderer.color = _colorGradient.Evaluate(ratio);

            if (_currentLifeTime > _lifeTime)
            {
                _isActive = false;
                PoolManager.Instance.Push(this);
            }
        }
        public void ResetItem()
        {
            _currentLifeTime = 0f;
        }
    }

}