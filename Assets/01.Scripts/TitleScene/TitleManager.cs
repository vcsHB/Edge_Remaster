using InputManage;
using UIManage;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace TitleScene
{

    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private string _inGameSceneName;
        [SerializeField] private UIPanel _sceneExitPanel;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _sceneExitDelay = 4f;

        public void SelectStage()
        {
            
        }

        public void MoveToGameScene()
        {
            _sceneExitPanel.Open();
            _playerInput.ResetInputEvents();
            Invoke(nameof(ExitScene), _sceneExitDelay);
        }

        private void ExitScene()
        {
            SceneManager.LoadScene(_inGameSceneName);

        }
    }
}