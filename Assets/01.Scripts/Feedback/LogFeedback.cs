using UIManage.TitleScene.LogSystem;
using UnityEngine;
namespace FeedbackSystem
{
    [RequireComponent(typeof(LogSender))]
    public class LogFeedback : Feedback
    {
        private LogSender _logSender;

        private void Awake()
        {
            _logSender = GetComponent<LogSender>();

        }
        public override void CreateFeedback()
        {
            _logSender.SendLog();
        }

        public override void FinishFeedback()
        {
        }
    }
}