using UnityEngine;
namespace Agents.Enemies.FSM
{

    public class EnemyAbilityState : EliteEnemyState
    {
        public EnemyAbilityState(Enemy owner, EnemyStateMachine stateMachine, int animationParam) : base(owner, stateMachine, animationParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _eliteAI.OnAbilityCompleteEvent += HandleAbilityComplete;
        }

        protected virtual void HandleAbilityComplete()
        {
            // State Change.
            _stateMachine.ChangeState("Idle");
        }
    }
}