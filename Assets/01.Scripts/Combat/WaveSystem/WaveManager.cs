using System.Collections;
using System.Collections.Generic;
using Agents.Enemies;
using Core.MapConrtrolSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Combat.WaveSystem
{
    public class WaveManager : MonoBehaviour
    {
        public UnityEvent OnWaveStartEvent;
        public UnityEvent OnWaveCompleteEvent;
        [SerializeField] private WaveListSO waveList;
        [SerializeField] private Transform _defaultSpawnPoint;

        [Header("Wave Detail Setting")] // Difficulty adjuster.
        [SerializeField] private float _amountMultiplier = 1f;
        [SerializeField] private float _levelMultiplier = 1f; 
        // It may need to be modified later depending on the direction of the game.
        private EnemyManager _enemyManager;
        private MapController _mapController;
        private List<Enemy> _enemyList;

        [SerializeField] private int _currentWave;

        private void Awake()
        {
            _enemyManager = FindAnyObjectByType<EnemyManager>();
            _mapController = FindAnyObjectByType<MapController>();
        }

        public void StartNextWave()
        {

        }

        private void SpawnEnemys(WaveSO wave)
        {
            StartCoroutine(SpawnEnemysCoroutine(wave));
        }

        private IEnumerator SpawnEnemysCoroutine(WaveSO wave)
        {
            for (int i = 0; i < wave.spawnGroups.Length; i++)
            {
                SpawnEnemyGroup(wave.spawnGroups[i]);
                yield return new WaitForSeconds(wave.spawnGroups[i].nextSpawnGroupTerm);
            }
        }
        private void SpawnEnemyGroup(SpawnGroup group)
        {
            switch (group.spawnType)
            {
                case SpawnType.SerialSpawn:
                    StartCoroutine(SpawnSerialType(group));
                    break;
                case SpawnType.ParallelSpawn:
                    SpawnParallelType(group);
                    break;
                case SpawnType.Boss:

                    break;
            }
        }

        private void SpawnParallelType(SpawnGroup group)
        {
            for (int i = 0; i < group.amount; i++)
            {
                Vector2 position = (Vector2)_defaultSpawnPoint.position + (Random.insideUnitCircle * group.spawnRandomizeRadius);
                GenerateEnemy(group.enemy, position);

            }
        }

        private IEnumerator SpawnSerialType(SpawnGroup group)
        {
            WaitForSeconds wait = new WaitForSeconds(group.spawnTerm);
            for (int i = 0; i < group.amount; i++)
            {
                Vector2 position = (Vector2)_defaultSpawnPoint.position + (Random.insideUnitCircle * group.spawnRandomizeRadius);
                GenerateEnemy(group.enemy, position);
                yield return wait;
            }
        }

        private void GenerateEnemy(EnemyTypeEnum enemyType, Vector2 position)
        {
            PoolableEnemy enemy = _enemyManager.Pop(enemyType, position, Quaternion.identity) as PoolableEnemy;
            enemy.OnDieEvent += HandleEnemyDie;
            _enemyList.Add(enemy);
        }

        private void HandleEnemyDie(Enemy enemy)
        {
            _enemyList.Remove(enemy);
            if (enemy is PoolableEnemy poolEnemy)
            {
                _enemyManager.Push(poolEnemy);

            }
            if (_enemyList.Count <= 0)
            {
                OnWaveCompleteEvent?.Invoke();
            }

        }


    }

}