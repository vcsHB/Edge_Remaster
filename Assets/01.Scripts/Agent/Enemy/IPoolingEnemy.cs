using System;
using Combat.WaveSystem;
using UnityEngine;

namespace Agents.Enemies
{
    public interface IPoolingEnemy
    {
        public Action<IPoolingEnemy> OnEnemyReturnToPoolEvent { get; set; }

        public GameObject PoolObject { get; }
        public EnemyTypeEnum EnemyType { get; set; }

        public void OnGenerated();

    }
}