using Agents.Enemies;
using Agents.Enemies.FSM;
using UnityEngine;
namespace Agnets.Enemies.AI
{

    public class EnemyDeadState : EnemyState
    {
        public EnemyDeadState(Enemy owner, EnemyStateMachine stateMachine, int animationParam) : base(owner, stateMachine, animationParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        
    }
}