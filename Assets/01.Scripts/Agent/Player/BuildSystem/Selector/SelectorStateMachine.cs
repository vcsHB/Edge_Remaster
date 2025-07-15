using System;
using System.Collections.Generic;
using UnityEngine;
namespace BuildSystem.SelectorManage.FSM
{

    public class SelectorStateMachine
    {
        private GridSelector _selector;
        private Dictionary<SelectorStateEnum, SelectorState> _stateDictionary = new();
        public SelectorState CurrentState { get; private set; }

        public SelectorStateMachine(GridSelector selector)
        {
            _selector = selector;

        }

        public void Initialize(SelectorStateEnum initState)
        {

            foreach (SelectorStateEnum stateEnum in Enum.GetValues(typeof(SelectorStateEnum)))
            {
                try
                {
                    Type type = Type.GetType($"BuildSystem.SelectorManage.FSM.Selector{stateEnum}State");
                    SelectorState state = Activator.CreateInstance(type, _selector, this) as SelectorState;
                    _stateDictionary.Add(stateEnum, state);

                    if (stateEnum == initState)
                        CurrentState = state;
                }
                catch (Exception error)
                {
                    Debug.LogError($"There is problem in SelectorFSM Reflection. Type:{stateEnum}, Error: {error}");
                }
            }
            CurrentState.Enter();
        }

        public void UpdateCurrentState()
        {
            CurrentState.UpdateState();
        }


        public void ChangeState(SelectorStateEnum stateType)
        {
            if (_stateDictionary.TryGetValue(stateType, out SelectorState state))
            {
                CurrentState.Exit();
                CurrentState = state;
                CurrentState.Enter();
            }
        }



    }
}
