using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Combat.Casters
{

    public class CircleRayCaster : Caster
    {
        [SerializeField] private int _rayPrecision = 15;
        [SerializeField] private float _detectRadius = 3f;

        private RaycastHit2D[] _raycastResults;
        private List<Collider2D> _hitBuffer;

        protected override void Awake()
        {
            base.Awake();
            _raycastResults = new RaycastHit2D[_targetMaxAmount];
            _hitBuffer = new List<Collider2D>(_rayPrecision);
        }

        public override void Cast()
        {
            base.Cast();

            Vector2 center = (Vector2)transform.position + _offset;
            float angleStep = 360f / _rayPrecision;

            _hitBuffer.Clear();

            for (int i = 0; i < _rayPrecision; i++)
            {
                float angle = angleStep * i;
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                int hitCount = Physics2D.RaycastNonAlloc(center, dir, _raycastResults, _detectRadius, _targetLayer);

                if (hitCount > 0)
                {
                    Collider2D hit = _raycastResults[0].collider;
                    if (hit != null && !_hitBuffer.Contains(hit))
                        _hitBuffer.Add(hit);
                }

#if UNITY_EDITOR
                Debug.DrawRay(center, dir * _detectRadius, _gizmosColor, 0.2f);
#endif
            }

            ForceCast(_hitBuffer.ToArray());
        }
    }
}