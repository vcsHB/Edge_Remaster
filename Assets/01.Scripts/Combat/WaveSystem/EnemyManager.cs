using System;
using System.Collections.Generic;
using Agents.Enemies;
using UnityEngine;
namespace Combat.WaveSystem
{

    public enum EnemyTypeEnum
    {
        Shooter,
        MegaShooter,
        Waver,
        TimeBreaker,
        Bomber,
        SpaceHolder
    }

    public class EnemyManager : MonoBehaviour
    {
        private Dictionary<EnemyTypeEnum, EnemyPool> _pools = new Dictionary<EnemyTypeEnum, EnemyPool>();
        public EnemyTableSO listSO;
        private List<IPoolingEnemy> _generatedObjects = new List<IPoolingEnemy>();

        private void Awake()
        {
            foreach (EnemySO item in listSO.datas)
            {
                CreatePool(item);
            }
        }
        private void CreatePool(EnemySO item)
        {
            var pool = new EnemyPool(item.prefab, item.prefab.EnemyType, transform, item.poolCount);
            _pools.Add(item.prefab.EnemyType, pool);
        }

        public IPoolingEnemy Pop(EnemyTypeEnum type)
        {
            if (_pools.ContainsKey(type) == false)
            {
                Debug.LogError($"Prefab dose not exist on Pool : {type}");
                return null;
            }

            IPoolingEnemy item = _pools[type].Pop();
            item.OnGenerated();
            _generatedObjects.Add(item);
            return item;
        }

        public IPoolingEnemy Pop(EnemyTypeEnum type, Vector3 position, Quaternion rotation)
        {
            IPoolingEnemy item = Pop(type);
            item.PoolObject.transform.position = position;
            item.PoolObject.transform.rotation = rotation;
            item.OnEnemyReturnToPoolEvent += HandleReturnEnemy;
            return item;
        }

        private void HandleReturnEnemy(IPoolingEnemy enemy)
        {
            Push(enemy);
        }

        public void Push(IPoolingEnemy obj, bool resetParent = false)
        {
            if (resetParent)
                obj.PoolObject.transform.SetParent(transform);
            _pools[obj.EnemyType].Push(obj);
            _generatedObjects.Remove(obj);
        }

        public void ResetPool()
        {
            foreach (IPoolingEnemy pool in _generatedObjects)
            {
                _pools[pool.EnemyType].Push(pool);
            }
            _generatedObjects.Clear();
        }

    }
}