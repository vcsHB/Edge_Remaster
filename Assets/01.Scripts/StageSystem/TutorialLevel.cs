using System;
using StageSystem.TutorialManage;
using UnityEngine;
namespace StageSystem
{

    public class TutorialLevel : StageLevel
    {
        [SerializeField] private TutorialStepController _stepController;
        [SerializeField] private float _stepStartDelay = 2f;

        private void Awake()
        {
            Invoke(nameof(HandleDelayedStartStep), _stepStartDelay);
        }

        private void HandleDelayedStartStep()
        {
            _stepController.StartStep();
            _stepController.OnAllStepOverEvent += HandleAllStepOver;
        }

        private void HandleAllStepOver()
        {
            InvokeMapInitOver();
        }
    }
}