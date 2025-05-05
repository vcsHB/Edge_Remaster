using System;
using Core;
using UnityEngine;
namespace UI.Common
{

    public class TimeControlPanel : MonoBehaviour
    {
        [SerializeField] private float _newTimeScale;
        private UIPanel _ownerPanel;

        private void Awake()
        {
            _ownerPanel = GetComponent<UIPanel>();
            _ownerPanel.OnOpenEvent.AddListener(HandlePanelOpen);
            _ownerPanel.OnCloseEvent.AddListener(HandlePanelClose);
        }

        private void HandlePanelOpen()
        {
            TimeManager.AddTimeScaleRecord(_newTimeScale);
        }

        private void HandlePanelClose()
        {
            TimeManager.RemoveTimeScaleRecord();
        }
    }
}