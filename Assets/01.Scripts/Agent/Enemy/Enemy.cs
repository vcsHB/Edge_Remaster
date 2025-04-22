using System;
using Agents.Enemies.FSM;
using Combat;

namespace Agents.Enemies
{
    public class Enemy : Agent
    {
        public int EnemyLevel { get; protected set; }
        public event Action<Enemy> OnDieEvent;
        public event Action<int> OnLevelSetEvent;
        public Health HealthCompo { get; protected set; }
        protected EnemyStateMachine _stateMachine;

        protected override void Awake()
        {
            base.Awake();
            HealthCompo = GetComponent<Health>();
            HealthCompo.OnDieEvent.AddListener(HandleEnemyDie);
            InitState();
        }

        public virtual void InitState()
        {
            _stateMachine = new EnemyStateMachine(this);
            _stateMachine.Initialize("Idle");
        }


        protected virtual void Update()
        {
            _stateMachine.UpdateState();
        }


        protected virtual void HandleEnemyDie()
        {
            OnDieEvent?.Invoke(this);
        }

        public virtual void SetLevel(int level)
        {
            EnemyLevel = level;
            OnLevelSetEvent?.Invoke(level);
        }
    }

}