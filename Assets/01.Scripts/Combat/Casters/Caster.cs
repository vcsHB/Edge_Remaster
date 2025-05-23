using UnityEngine;
using UnityEngine.Events;
namespace Combat.Casters
{
    public class CasterData
    {
        // Data Capsule Grouping Class
    }

    public abstract class Caster : MonoBehaviour
    {
        [Tooltip("Calls by 1 Cast")]
        public UnityEvent OnCastEvent;
        [Tooltip("Calls when Successed Cesting")]
        public UnityEvent OnCastSuccessEvent;

        [Space(10f)]
        [Header("Setting Values")]
        [SerializeField] protected Vector2 _offset;
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected int _targetMaxAmount;
        
        [Header("Gizmos Setting")]
        [SerializeField] protected Color _gizmosColor = Color.red;

        protected ICastable[] _casters;
        protected Collider2D[] _hits;
        public Vector2 CenterPosition => (Vector2)transform.position + _offset;

        protected virtual void Awake()
        {
            _casters = GetComponentsInChildren<ICastable>();
        }

        public virtual void Cast()
        {
            OnCastEvent?.Invoke();
        }

        public void ForceCast(Collider2D[] hit)
        {
            for (int i = 0; i < hit.Length; i++)
                ForceCast(hit[i]);
        }

        public void ForceCast(Collider2D hit)
        {
            for (int j = 0; j < _casters.Length; j++)
                _casters[j].Cast(hit);

            OnCastSuccessEvent?.Invoke();
        }

        public void SendCasterData(CasterData data)
        {
            foreach (ICastable caster in _casters)
                caster.HandleSetData(data);
        }
        public void SetTargetLayer(LayerMask whatIsTarget)
        {
            _targetLayer = whatIsTarget;
        }
    }
}