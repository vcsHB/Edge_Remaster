using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UI.Core
{
    public enum CanvasType
    {
        Game,
        System,
        Event,
        Other
    }
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas _mainCanvas;
        private Transform _mainCanvasTrm;
        private Dictionary<CanvasType, UIGroup> _uiGroupDictionary = new();

        private void Awake()
        {
            _mainCanvasTrm = _mainCanvas.transform;
            foreach (CanvasType type in Enum.GetValues(typeof(CanvasType)))
            {
                _uiGroupDictionary.Add(
                    type,
                    _mainCanvasTrm.Find($"{type.ToString()}Group").GetComponent<UIGroup>());
            }
        }

        public void OpenUIGroup(CanvasType canvasType)
        {
            if (_uiGroupDictionary.TryGetValue(canvasType, out UIGroup uiGroup))
            {
                uiGroup.Open();
            }
            else
                Debug.LogError("Not Exist UI Group");
        }

        public void CloseUIGroup(CanvasType canvasType)
        {
            if (_uiGroupDictionary.TryGetValue(canvasType, out UIGroup uiGroup))
            {
                uiGroup.Close();
            }
            else
                Debug.LogError("Not Exist UI Group");
        }
    }
}