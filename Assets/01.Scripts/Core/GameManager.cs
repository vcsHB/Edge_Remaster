using System;
using System.Collections;
using Combat.WaveSystem;
using Core.DataManage;
using Core.MapConrtrolSystem;
using Core.VolumeControlSystem;
using InputManage;
using StageSystem;
using UIManage.Core;
using UIManage.InGame;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public UnityEvent OnPlayerArriveEvent;
        [SerializeField] private PlayerDiePanel _playerDiePanel;
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] private PlayerInput _playerInput;
        private PlayerManager _playerManager;
        private MapController _mapController;
        private StageManager _stageManager;
        private UIManager _uiManager;
        private VolumeManager _volumeManager;
        [SerializeField] private float _cutSceneDelay = 0.3f;
        [SerializeField] private float _playerForceMoveDuration = 0.6f;
        [SerializeField] private PlayerDiePanel _clearPanel; // Reuse. must refactor
        private StageDataSO _currentStageData;

        private void Awake()
        {
            DataManager.Load();
            _playerManager = FindFirstObjectByType<PlayerManager>();
            _mapController = FindFirstObjectByType<MapController>();
            _uiManager = FindFirstObjectByType<UIManager>();
            _volumeManager = FindFirstObjectByType<VolumeManager>();
            _stageManager = FindFirstObjectByType<StageManager>();

            _currentStageData = _stageManager.InitializeStage(DataManager.stageDataGroup.enterStageId);
            _stageManager.OnWaveStartEvent += () =>
            {
                _waveManager.SetWaveData(_currentStageData.waveSet);
            };
            _waveManager.OnWaveAllClearEvent.AddListener(ClearGame);

        }

        private void Start()
        {
            _playerManager.Player.OnPlayerDieEvent.AddListener(HandlePlayerDie);
            StartCoroutine(GameStartCutSceneCoroutine());
        }

        private void HandlePlayerDie()
        {
            TimeManager.AddTimeScaleRecord(0.2f);

            _playerDiePanel.Open();
            _playerInput.ResetInputEvents();
        }

        private IEnumerator GameStartCutSceneCoroutine()
        {
            yield return new WaitForSeconds(_cutSceneDelay);
            _playerManager.ForceMovePlayer(
                _mapController.GetRandomPoint(),
                _playerForceMoveDuration);
            yield return new WaitForSeconds(_playerForceMoveDuration);
            _uiManager.OpenUIGroup(CanvasType.Game);
            OnPlayerArriveEvent?.Invoke();

        }

        public void ClearGame()
        {
            TimeManager.AddTimeScaleRecord(0.2f);
            _clearPanel.Open();
            DataManager.stageDataGroup.datas[_currentStageData.id].isCleared = true;
            DataManager.stageDataGroup.datas[_currentStageData.id +1].isUnlocked = true;
        }

        public void ExitGame()
        {
            DataManager.Save();
            TimeManager.ResetTimeScale();
            SceneManager.LoadScene("TitleScene");
        }
    }
}