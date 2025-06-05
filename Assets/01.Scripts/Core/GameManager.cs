using System.Collections;
using Core.MapConrtrolSystem;
using Core.VolumeControlSystem;
using UIManage.Core;
using UIManage.InGame;
using UnityEngine;
using UnityEngine.Events;
namespace Core
{

    public class GameManager : MonoBehaviour
    {
        public UnityEvent OnPlayerArriveEvent;
        [SerializeField] private PlayerDiePanel _playerDiePanel;
        private PlayerManager _playerManager;
        private MapController _mapController;
        private UIManager _uiManager;
        private VolumeManager _volumeManager;
        [SerializeField] private float _cutSceneDelay = 0.3f;
        [SerializeField] private float _playerForceMoveDuration = 0.6f;

        private void Awake()
        {
            _playerManager = FindFirstObjectByType<PlayerManager>();
            _mapController = FindFirstObjectByType<MapController>();
            _uiManager = FindFirstObjectByType<UIManager>();
            _volumeManager = FindFirstObjectByType<VolumeManager>();
            _playerManager.Player.OnPlayerDieEvent.AddListener(_playerDiePanel.Open);
        }

        private void Start()
        {
            StartCoroutine(GameStartCutSceneCoroutine());
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
    }
}