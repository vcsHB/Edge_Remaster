using Agents.Players.Combat;
using Unity.VisualScripting;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerNoLimitState : PlayerState
    {
        protected PlayerLimiter _limiter;
        protected PlayerAttackController _attackController;
        public PlayerNoLimitState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
            _limiter = _player.GetCompo<PlayerLimiter>();
            _attackController = _player.GetCompo<PlayerAttackController>();
        }



    }
}