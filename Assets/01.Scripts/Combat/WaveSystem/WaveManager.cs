using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Agents.Enemies;
using Core.MapConrtrolSystem;
using ObjectManage;
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
        [SerializeField] private float _waveStartDelay = 5f;
        [SerializeField] private float _amountMultiplier = 1f;
        [SerializeField] private float _levelMultiplier = 1f;
        // It may need to be modified later depending on the direction of the game.
        private EnemyManager _enemyManager;
        private MapController _mapController;
        private List<IPoolingEnemy> _enemyList = new();

        [SerializeField] private int _currentWaveIndex;
        [SerializeField] private int _waveLevel = 0;
        private Coroutine _waveCoroutine;

        private void Awake()
        {
            _enemyManager = FindAnyObjectByType<EnemyManager>();
            _mapController = FindAnyObjectByType<MapController>();
        }

        private void Start()
        {
            _waveCoroutine = StartCoroutine(WaveCoroutine());
        }

        private IEnumerator WaveCoroutine()
        {
            yield return new WaitForSeconds(_waveStartDelay);

            while (true)
            {
                _currentWaveIndex = 0;  // Loop Control
                while (_currentWaveIndex < waveList.waves.Length)
                {
                    WaveSO currentWave = waveList.waves[_currentWaveIndex]; // Wave Spawn Sycle
                    OnWaveStartEvent?.Invoke();

                    yield return SpawnEnemys(currentWave);
                    yield return new WaitUntil(() => _enemyList.Count == 0); // Wait for AllKill
                    OnWaveCompleteEvent?.Invoke();
                    yield return new WaitForSeconds(currentWave.waveTerm);

                    _currentWaveIndex++;
                }
                _waveLevel++;
            }
        }



        private Coroutine SpawnEnemys(WaveSO wave)
        {
            return StartCoroutine(SpawnEnemysCoroutine(wave));
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
                    // not yet
                    break;
            }
        }

        private void SpawnParallelType(SpawnGroup group)
        {
            int amount = group.amount + _waveLevel;
            for (int i = 0; i < amount; i++)
            {
                print("Parallel");
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
            enemy.OnEnemyReturnToPoolEvent += HandleEnemyDie;
            _enemyList.Add(enemy);

            VFXPlayer vfxPlayer = PoolManager.Instance.Pop(ObjectPooling.PoolingType.EnemyGenerateVFX) as VFXPlayer;
            vfxPlayer.transform.position = position;
            vfxPlayer.Play();
        }

        private void HandleEnemyDie(IPoolingEnemy enemy)
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