using UnityEngine;

namespace Combat
{

    public class DamageCaster : MonoBehaviour, ICastable
    {
        [SerializeField] private float _damage;

        public void SetDamage(float value)
        {
            _damage = value;
        }

        public void Cast(Collider2D target)
        {
            if(target.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage);
            }
        }


    }

}