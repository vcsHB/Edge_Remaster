using UnityEngine;
namespace Agents.Players.FSM
{



    public class PlayerNoLimitEnterState : PlayerNoLimitState
    {
        public PlayerNoLimitEnterState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.SetEdgeMode(false);
            _limiter.SetUnLimit();
            _stateMachine.ChangeState("NoLimitIdle");
            _attackController.SelectWeapon(1);
        
        }


        


    }
}