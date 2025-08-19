using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
namespace StageSystem.TutorialManage
{

    public class TutorialStep : MonoBehaviour
    {
        public UnityEvent OnStepEnterUnityEvent;
        public UnityEvent OnStepExitUnityEvent;
        public event Action<TutorialStep> OnStepEnterEvent;
        public event Action<TutorialStep> OnStepExitEvent;
        [SerializeField] private DescriptionObject[] _descriptionObjects;
        private bool _isStepOver;
        public bool IsStepOver => _isStepOver;

        public virtual void Enter()
        {
            OnStepEnterUnityEvent?.Invoke();
            OnStepEnterEvent?.Invoke(this);
            for (int i = 0; i < _descriptionObjects.Length; i++)
            {
                _descriptionObjects[i].Open();
            }
        }

        public virtual void Exit()
        {
            OnStepExitUnityEvent?.Invoke();
            OnStepExitEvent?.Invoke(this);
            _isStepOver = true;
            for (int i = 0; i < _descriptionObjects.Length; i++)
            {
                _descriptionObjects[i].Close();
            }
        }


    }
}