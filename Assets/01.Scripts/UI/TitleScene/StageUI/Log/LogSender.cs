using Core.EventSystem;
using UnityEngine;
namespace UIManage.TitleScene.LogSystem
{

    public class LogSender : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _logEventChannel;
        [SerializeField] private LogContent _content;
        public void SendLog()
        {
            _logEventChannel.RaiseEvent(_content);
        }
    }
}