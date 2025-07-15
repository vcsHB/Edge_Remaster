using UnityEngine;
namespace Combat.Casters
{

    public class BoxCaster : Caster
    {
        [SerializeField] private Vector2 _boxSize;
        [SerializeField] private float _angle;

        [ContextMenu("DebugCast")]
        public override void Cast()
        {
            base.Cast();
            _hits = Physics2D.OverlapBoxAll(CenterPosition, _boxSize, _angle, _targetLayer);
            ForceCast(_hits);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireCube(CenterPosition, _boxSize);
        }
#endif
    }
}