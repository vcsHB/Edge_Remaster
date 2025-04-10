using Agents.Players;
using Combat;
using ObjectPooling;
using UnityEngine;

public class BulletDamageCaster : MonoBehaviour, ICastable
{
    [SerializeField] private float _damage;
    public void Cast(Collider2D target)
    {
        if (target.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(_damage);
            IPoolable obj = transform.root.GetComponentInChildren<IPoolable>();
            PoolManager.Instance.Push(obj);
        }
    }
}
