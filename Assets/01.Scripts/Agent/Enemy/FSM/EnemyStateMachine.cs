using System;
using System.Collections.Generic;
namespace Agents.Enemies.FSM
{

    public class EnemyStateMachine
    {
        protected Enemy _owner;
        public EnemyState CurrentState { get; protected set; }
        public Dictionary<string, EnemyState> _stateDictionary = new();
        public EnemyStateMachine(Enemy owner)
        {
            _owner = owner;
        }

        public virtual void Initialize(string startState)
        {
            CurrentState = _stateDictionary[startState];

        }

        public void UpdateState()
        {
            CurrentState.Update();
            //Debug.Log(CurrentState);
        }

        public void AddState(string id, string typeName, int animParam)
        {
            Type t = Type.GetType($"Agents.Enemies.FSM.{typeName}State");
            EnemyState state = Activator.CreateInstance(t, _owner, this, animParam) as EnemyState;
            _stateDictionary.Add(id, state);
        }

        public void ChangeState(string stateName, bool isForce = false)
        {
            if (_stateDictionary.TryGetValue(stateName, out EnemyState newState))
            {
                CurrentState.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
        }
    }
}