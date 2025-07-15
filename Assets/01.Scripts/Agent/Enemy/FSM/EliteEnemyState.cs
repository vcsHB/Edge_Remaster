using UnityEngine;
namespace Agents.Enemies.FSM
{

    public class EliteEnemyState : EnemyState
    {
        protected EliteEnemyAI _eliteAI;
        protected bool _canUseAbilityState;
        public EliteEnemyState(Enemy owner, EnemyStateMachine stateMachine, int animationParam) : base(owner, stateMachine, animationParam)
        {
            _eliteAI = owner.GetCompo<EliteEnemyAI>();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            if (_canUseAbilityState)
                if (_eliteAI.UpdateAbility())
                    SetAvilityStateEnable();

        }

        public override void Exit()
        {
            base.Exit();

        }

        protected virtual void SetAvilityStateEnable()
        {
            _stateMachine.ChangeState("Ability");
        }
    }
}