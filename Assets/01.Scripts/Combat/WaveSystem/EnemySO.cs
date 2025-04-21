using Agents.Enemies;
using UnityEngine;
namespace Combat.WaveSystem
{

    [CreateAssetMenu(menuName = "SO/WaveSystem/EnemySO")]
    public class EnemySO : ScriptableObject
    {
        public GameObject prefabObject;
        public EnemyTypeEnum type;
        public int poolCount;

        public IPoolingEnemy prefab = null; 

        private void OnValidate()
        {
            CheckInterface();
        }

        private void OnEnable()
        {
            CheckInterface();
        }

        private void CheckInterface()
        {
            if (prefabObject != null)
            {
                if (prefabObject.TryGetComponent(out IPoolingEnemy poolable))
                {
                    prefab = poolable;
                }
                else
                {
                    prefabObject = null;
                    Debug.LogWarning("Not Poolable");
                }
               
            }
        }
    }
}