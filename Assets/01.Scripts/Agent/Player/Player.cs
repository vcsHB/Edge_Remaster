using Agents.Players.FSM;
using InputManage;
using StatSystem;
using UnityEngine;
namespace Agents.Players
{
    public class Player : Agent
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
        private PlayerStateMachine _stateMachine;
        public PlayerStateMachine StateMachine => _stateMachine;
        [field: SerializeField] public PlayerStatusSO PlayerStatus { get; private set; }
        public Health HealthCompo { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            PlayerStatus = Instantiate(PlayerStatus);
            HealthCompo = GetComponent<Health>();
            HealthCompo.Initialize(PlayerStatus.health.GetValue());
            _stateMachine = new PlayerStateMachine(this);
            _stateMachine.Initialize("Idle");
        }

        private void Update()
        {
            _stateMachine.UpdateState();
        }

    }
}