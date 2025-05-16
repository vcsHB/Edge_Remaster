using UnityEngine;
namespace Agents.Enemies.FSM
{

    public class EliteEnemyState : EnemyState
    {
        protected EliteEnemyAI _eliteAI;
        public EliteEnemyState(Enemy owner, EnemyStateMachine stateMachine, int animationParam) : base(owner, stateMachine, animationParam)
        {
            _eliteAI = owner.GetCompo<EliteEnemyAI>();
        }
    }
}