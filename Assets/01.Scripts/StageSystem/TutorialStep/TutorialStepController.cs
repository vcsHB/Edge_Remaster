using System;
using System.Collections;
using UnityEngine;
namespace StageSystem.TutorialManage
{

    public class TutorialStepController : MonoBehaviour
    {
        [SerializeField] private TutorialStep[] _steps;
        [SerializeField] private float _stepTerm;
        [SerializeField] private int _currentPregressIndex;
        public event Action OnAllStepOverEvent;

        public void StartStep()
        {
            _currentPregressIndex = 0;
            _steps[_currentPregressIndex].OnStepExitEvent += HandleStepEnd;
            _steps[_currentPregressIndex].Enter();

        }

        private void HandleStepEnd(TutorialStep currentEndStep)
        {
            currentEndStep.OnStepExitEvent -= HandleStepEnd;

            StartCoroutine(StepChangeCoroutine());           

        }

        private IEnumerator StepChangeCoroutine()
        {
            yield return new WaitForSeconds(_stepTerm);
            _currentPregressIndex++;
            if (_currentPregressIndex >= _steps.Length) 
            {
                OnAllStepOverEvent?.Invoke();
                yield break;
            }
            TutorialStep step = _steps[_currentPregressIndex];
            step.OnStepExitEvent += HandleStepEnd;
            step.Enter();
        }
    }
}