using UnityEngine;
namespace Combat.WaveSystem
{
    public enum SpawnType
    {
        SerialSpawn,
        ParallelSpawn,
        Boss
    }


    [System.Serializable]
    public class SpawnGroup
    {
        public SpawnType spawnType;
        public EnemyTypeEnum enemy;
        public int amount;
        public float spawnTerm;
        public float spawnRandomizeRadius;
        public float nextSpawnGroupTerm;
    }
    [CreateAssetMenu(menuName = "SO/WaveSystem/WaveSO")]
    public class WaveSO : ScriptableObject
    {
        public SpawnGroup[] spawnGroups;
        public float waveTerm;
    }
}