using Core.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UIManage.TitleScene.LogSystem
{
    [System.Serializable]
    public class LogContent : GameEvent
    {

        public string content;
        public Color logColor;
        public Sprite logIcon;

    }
    public class LogItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _logContentText;
        [SerializeField] private Image _logIconImage;

        public void SetEnable(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetLogContent(LogContent newContent)
        {
            _logIconImage.sprite = newContent.logIcon;
            _logIconImage.color = newContent.logColor;
            _logContentText.text = newContent.content;
            _logContentText.color = newContent.logColor;
        }   
    }
}