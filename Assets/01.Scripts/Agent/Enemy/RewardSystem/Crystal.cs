using ObjectPooling;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using LevelSystem;

namespace RewardSystem
{

    public class Crystal : MonoBehaviour, IPoolable
    {
        public UnityEvent OnCollectedEvent;
        [Header("Generate Setting")]
        [SerializeField] private float _explosionMinPower = 0.9f;
        [SerializeField] private float _explosionMaxPower = 1.3f;
        [SerializeField] private float _explosionDuration = 0.4f;
        [field: SerializeField] public PoolingType type { get; set; }
        [SerializeField] private int _collectAmount;
        [SerializeField] private Color _minColor = Color.yellow;
        [SerializeField] private Color _maxColor = Color.cyan;
        [Header("Follow Settings")]
        [SerializeField] private float _rotationSpeedMultiplier = 3f;
        [SerializeField] private float _followSpeed;
        private float _defaultFollowSpeed;
        [SerializeField] private float _speedIncreaseRate;
        [SerializeField] private float _collectorDetectRadius = 6f;
        [SerializeField] private float _collectorGainRadius = 0.4f;
        [SerializeField] private LayerMask _collectTarget;
        private Transform _targetTrm;
        private Transform _visualTrm;
        private SpriteRenderer _visualRenderer;
        private bool _isTargetDetected;
        private readonly int _colorHash = Shader.PropertyToID("_Color");
        private bool _canFollow;

        public GameObject ObjectPrefab => gameObject;

        private void Awake()
        {
            _defaultFollowSpeed = _followSpeed;
            _visualTrm = transform.Find("Visual");
            _visualRenderer = _visualTrm.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_canFollow)
                DetectCollector();
        }

        private void FixedUpdate()
        {
            if (_canFollow)
                FollowTarget();
        }

        public void Drop(Vector2 position, int amount, Vector2 direction)
        {
            transform.position = position;
            Vector2 randomDirection = direction.normalized * UnityEngine.Random.Range(_explosionMinPower, _explosionMaxPower);
            _visualTrm.up = -direction;
            transform.DOMove(
                position + randomDirection,
                _explosionDuration).SetEase(Ease.OutExpo).OnComplete(() => _canFollow = true);

            _collectAmount = Mathf.Clamp(amount, 0, 100);
            _visualRenderer.material.SetColor(_colorHash, Color.Lerp(_minColor, _maxColor, (float)_collectAmount / 100));
        }

        private void FollowTarget()
        {
            if (_targetTrm == null) return;
            Vector3 direction = _targetTrm.position - transform.position;
            _followSpeed += _speedIncreaseRate * Time.deltaTime;
            float rotation = _followSpeed * _rotationSpeedMultiplier;
            _visualTrm.up = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation));
            transform.position += direction.normalized * _followSpeed * Time.fixedDeltaTime;
        }

        private void DetectCollector()
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, _collectorDetectRadius, _collectTarget);
            if (target == null) return;
            _targetTrm = target.transform;
            _isTargetDetected = true;
            Collider2D collector = Physics2D.OverlapCircle(transform.position, _collectorGainRadius, _collectTarget);
            if (collector != null)
            {
                HandleCollected();
            }
        }

        private void HandleCollected()
        {
            OnCollectedEvent?.Invoke();
            PoolManager.Instance.Push(this);
            LevelManager.Instance.AddCrystal(_collectAmount);
        }

        public void ResetItem()
        {
            _canFollow = false;
            _targetTrm = null;
            _followSpeed = _defaultFollowSpeed;
        }
#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _collectorGainRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _collectorDetectRadius);
        }

#endif

    }
}