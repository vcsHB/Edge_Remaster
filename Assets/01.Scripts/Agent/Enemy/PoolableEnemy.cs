using Agents.Enemies;
using Combat.WaveSystem;
using UnityEngine;
namespace Agents.Enemies
{

    public abstract class PoolableEnemy : Enemy, IPoolingEnemy
    {
        public GameObject PoolObject => gameObject;
      
        [field: SerializeField]  public EnemyTypeEnum EnemyType { get; set; }
        public virtual void OnGenerated()
        {
        }
    }
}