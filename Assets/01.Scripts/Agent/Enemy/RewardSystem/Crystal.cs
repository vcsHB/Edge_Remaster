using System;
using UnityEngine;
namespace RewardSystem
{

    public class Crystal : MonoBehaviour
    {
        [SerializeField] private int _collectAmount;
        [SerializeField] private Color _minColor = Color.yellow;
        [SerializeField] private Color _maxColor = Color.cyan;
        [Header("Follow Settings")]
        [SerializeField] private float _followSpeed;
        [SerializeField] private float _collectorDetectRadius = 6f;
        [SerializeField] private float _collectorGainRadius = 0.4f;
        [SerializeField] private LayerMask _collectTarget;
        private Transform _targetTrm;
        private Transform _visualTrm;
        private SpriteRenderer _visualRenderer;
        private bool _isTargetDetected;
        private readonly int _colorHash = Shader.PropertyToID("_Color");

        private void Awake()
        {
            _visualTrm = transform.Find("Visual");
            _visualRenderer = _visualTrm.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!_isTargetDetected)
                DetectCollector();
        }

        private void FixedUpdate()
        {
            if (!_isTargetDetected)
                FollowTarget();
        }

        public void Drop(int amount)
        {
            _collectAmount = Mathf.Clamp(amount, 0, 100);
            _visualRenderer.material.SetColor(_colorHash, Color.Lerp(_minColor, _maxColor, (float)_collectAmount / 100));
        }

        private void FollowTarget()
        {
            Vector3 direction = _targetTrm.position - transform.position;
            _targetTrm.position += direction.normalized * _followSpeed * Time.fixedDeltaTime;
        }

        private void DetectCollector()
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, _collectorDetectRadius, _collectTarget);
            if (target == null) return;
            _targetTrm = target.transform;
            _isTargetDetected = true;
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