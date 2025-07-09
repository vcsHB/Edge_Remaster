using Agents.Enemies.FSM;
using UnityEngine;

namespace Agents.Enemies
{
    public class CrabEnemy : PoolableEnemy
    {
        public override void InitState()
        {
            base.InitState();
            _stateMachine = new EnemyStateMachine(this);
            _stateMachine.AddState("Idle", "EnemyIdle", 1);
            _stateMachine.AddState("Move", "EnemyMoveToTarget", 1);
            _stateMachine.AddState("Dead", "EnemyDead", 1);
            _stateMachine.Initialize("Idle");
        }

        


    }
}
