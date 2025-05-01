using Agents.Players.FSM;
using Combat;
using InputManage;
using ObjectManage;
using StatSystem;
using UnityEngine;
namespace Agents.Players
{
    public class Player : Agent
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
        private PlayerStateMachine _stateMachine;
        public PlayerStateMachine StateMachine => _stateMachine;
        public PlayerStatusSO PlayerStatus { get; private set; }
        public Health HealthCompo { get; private set; }
        public PlayerMover MovementCompo { get; private set; }
        private AgentStat _statCompo;

        protected override void Awake()
        {
            base.Awake();
            _statCompo = GetCompo<AgentStat>();
            _statCompo.CloneStatus();
            PlayerStatus = _statCompo.Status as PlayerStatusSO;
            PlayerStatus = Instantiate(PlayerStatus);

            MovementCompo = GetCompo<PlayerMover>();
            HealthCompo = GetComponent<Health>();
            HealthCompo.Initialize(PlayerStatus.health.GetValue());
            _stateMachine = new PlayerStateMachine(this);
            _stateMachine.Initialize("Idle");
        }

        private void Update()
        {
            _stateMachine.UpdateState();
        }

        public void ForceMoveToPoint(MovePoint movePoint, float duration)
        {
            MovementCompo.ForceMoveToPosition(movePoint, duration);
        }

    }
}