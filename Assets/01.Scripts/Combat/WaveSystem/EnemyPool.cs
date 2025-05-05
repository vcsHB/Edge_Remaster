using System.Collections.Generic;
using Combat.WaveSystem;
using UnityEngine;

namespace Agents.Enemies
{
    public class EnemyPool
    {
        private Queue<IPoolingEnemy> _pool = new Queue<IPoolingEnemy>();
        private IPoolingEnemy _prefab;
        private Transform _parent;
        
        private EnemyTypeEnum _type;

        public EnemyPool(IPoolingEnemy prefab, EnemyTypeEnum type, Transform parent, int count)
        {
            _prefab = prefab;
            _type = type;
            _parent = parent;

            for (int i = 0; i < count; i++)
            {
                GameObject obj = GameObject.Instantiate(_prefab.PoolObject, _parent);
                
                obj.gameObject.name = _type.ToString();
                obj.gameObject.SetActive(false);
                IPoolingEnemy item = obj.GetComponent<IPoolingEnemy>();
                item.EnemyType = _type;
                _pool.Enqueue(item);
            }
        }

        public IPoolingEnemy Pop()
        {
            IPoolingEnemy item;
            if (_pool.Count <= 0)
            {
                // 부족하면 새로 보충하는 부분
                GameObject obj = GameObject.Instantiate(_prefab.PoolObject, _parent);
                obj.gameObject.name = _type.ToString();
                item = obj.GetComponent<IPoolingEnemy>();
                item.EnemyType = _type;
            }
            else
            {
                item = _pool.Dequeue();
                item.PoolObject.SetActive(true);
            }

            return item;
        }
        
        public void Push(IPoolingEnemy obj)
        {
            obj.PoolObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}