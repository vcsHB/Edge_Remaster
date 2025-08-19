using System.Collections;
using System.Collections.Generic;
using Core.EventSystem;
using UnityEngine;
namespace UIManage.TitleScene.LogSystem
{

    public class LogController : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _logEventChannel;

        [SerializeField] private LogItem _logItemPrefab;
        private Queue<LogItem> _logItemPool;

        [SerializeField] private Transform _contentTrm;
        private List<LogItem> _enabledLogItemList = new();



        public void GenerateLog(LogContent content)
        {
            LogItem newItem = _logItemPool.Count > 0 ?
                _logItemPool.Dequeue() : Instantiate(_logItemPrefab, _contentTrm);

            newItem.SetEnable(true);
            _enabledLogItemList.Add(newItem);
            newItem.SetLogContent(content);
        }

        public void ResetLogs()
        {
            for (int i = 0; i < _enabledLogItemList.Count; i++)
            {
                _enabledLogItemList[i].SetEnable(false);

            }
        }
    }
}