using System;
using System.Collections;
using System.Collections.Generic;
using Agents.Enemies;
using Core.MapConrtrolSystem;
using ObjectManage;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Combat.WaveSystem
{
    public class WaveManager : MonoBehaviour
    {
        public event Action<int, float> OnWaveLeftTimeEvent; // leftTime, ratio
        public event Action OnWaveCycleInitEvent;
        public UnityEvent OnWaveStartEvent;
        public UnityEvent OnWaveCompleteEvent;
        public UnityEvent OnWaveAllClearEvent;
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
        [SerializeField] private int _waveSequenceIndex = 0;
        [SerializeField] private int _waveLevel;
        private Coroutine _waveCoroutine;

        private void Awake()
        {
            _enemyManager = FindAnyObjectByType<EnemyManager>();
            _mapController = FindAnyObjectByType<MapController>();
        }

        private void Start()
        {
        }

        private IEnumerator WaveCoroutine()
        {
            OnWaveCycleInitEvent?.Invoke();
            // Init   ===========================
            float initCurrentTime = 0f;
            while (initCurrentTime < _waveStartDelay)
            {
                OnWaveLeftTimeEvent?.Invoke((int)(_waveStartDelay - initCurrentTime), initCurrentTime / _waveStartDelay);
                initCurrentTime += Time.deltaTime;
                yield return null;
            }
            OnWaveLeftTimeEvent?.Invoke(0, 1);
            // Init   ===========================

            _currentWaveIndex = 0;  // Loop Control
            while (_currentWaveIndex < waveList.waves.Length)
            {
                WaveSO currentWave = waveList.waves[_currentWaveIndex]; // Wave Spawn Cycle
                OnWaveStartEvent?.Invoke();

                yield return SpawnEnemys(currentWave);
                yield return new WaitUntil(() => _enemyList.Count == 0); // Wait for AllKill
                OnWaveCompleteEvent?.Invoke();

                if (_currentWaveIndex >= waveList.waves.Length - 1)
                    break;

                float currentTime = 0;
                while (currentTime < currentWave.waveTerm)
                {
                    OnWaveLeftTimeEvent?.Invoke((int)(currentWave.waveTerm - currentTime), currentTime / currentWave.waveTerm);
                    currentTime += Time.deltaTime;
                    yield return null;
                }
                OnWaveLeftTimeEvent?.Invoke(0, 1);

                _waveSequenceIndex++;
                _currentWaveIndex++;
                _waveLevel = (int)waveList.levelFormula.Evaluate(_waveSequenceIndex);
            }

            Debug.Log("Clear");
            OnWaveAllClearEvent?.Invoke();
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
                Vector2 position = (Vector2)_defaultSpawnPoint.position + (Random.insideUnitCircle * group.spawnRandomizeRadius);
                GenerateEnemy(group.enemy, position);

            }
        }

        private IEnumerator SpawnSerialType(SpawnGroup group)
        {
            WaitForSeconds wait = new WaitForSeconds(group.spawnTerm);
            int amount = group.amount + _waveLevel;
            for (int i = 0; i < amount; i++)
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
            enemy.SetLevel(_waveSequenceIndex);
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

        public void SetWaveData(WaveListSO waveList)
        {
            this.waveList = waveList;
            if (this.waveList != null)
                _waveCoroutine = StartCoroutine(WaveCoroutine());
        }
    }

}