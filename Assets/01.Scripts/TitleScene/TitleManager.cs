using UnityEngine;
using UnityEngine.SceneManagement;
namespace TitleScene
{

    public class TitleManager : MonoBehaviour
    {
        [SerializeField] private string _inGameSceneName;

        
        public void MoveToGameScene()
        {
            SceneManager.LoadScene(_inGameSceneName);
        }
    }
}