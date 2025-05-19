using Agents.Enemies.FSM;
using UnityEngine;

namespace Agents.Enemies
{
    public class TimeBreaker : PoolableEnemy
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public override void InitState()
        {
            _stateMachine = new EnemyStateMachine(this);
            _stateMachine.AddState("Idle", "EnemyIdle", 1);
            _stateMachine.AddState("Move", "EliteEnemyMoveToTarget", 1);
            _stateMachine.AddState("Dead", "EnemyDead", 1);
            _stateMachine.AddState("Ability", "EnemyAbility", 1);
            _stateMachine.Initialize("Idle");
        }

    }
}