using Combat.WaveSystem;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies
{

    public abstract class PoolableEnemy : Enemy, IPoolingEnemy
    {
        public UnityEvent OnSpawnEvent;
        public GameObject PoolObject => gameObject;
      
        [field: SerializeField]  public EnemyTypeEnum EnemyType { get; set; }

        public override void OnGenerated()
        {
            base.OnGenerated();
            OnSpawnEvent?.Invoke();
        }
    }
}