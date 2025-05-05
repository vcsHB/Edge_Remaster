using System;
using Combat.WaveSystem;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies
{

    public abstract class PoolableEnemy : Enemy, IPoolingEnemy
    {
        public UnityEvent OnSpawnEvent;
        public GameObject PoolObject => gameObject;

        [field: SerializeField] public EnemyTypeEnum EnemyType { get; set; }
        public Action<IPoolingEnemy> OnEnemyReturnToPoolEvent { get; set; }

        internal void HandleReturnToPool()
        {
            OnEnemyReturnToPoolEvent?.Invoke(this);
        }

        public override void OnGenerated()
        {
            base.OnGenerated();
            HealthCompo.ResetHealth();
            OnSpawnEvent?.Invoke();
        }




    }
}