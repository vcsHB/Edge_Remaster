using System;
using Agents.Enemies.FSM;
using Combat;
using ObjectManage;
using ObjectPooling;
using UnityEngine;

namespace Agents.Enemies
{
    public class Enemy : Agent
    {
        [SerializeField] protected PoolingType _destroyVFXType;
        public int EnemyLevel { get; protected set; }
        public event Action<Enemy> OnDieEvent;
        public event Action<int> OnLevelSetEvent;
        public event Action OnGeneratedEvent;
        public Health HealthCompo { get; protected set; }
        protected EnemyStateMachine _stateMachine;
        private EnemyRenderer _enemyRenderer;

        protected override void Awake()
        {
            base.Awake();
            HealthCompo = GetComponent<Health>();
            HealthCompo.OnDieEvent.AddListener(HandleEnemyDie);
            _enemyRenderer = GetCompo<EnemyRenderer>();
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
            _enemyRenderer.SetDeadColor(true);
            VFXPlayer vfx = PoolManager.Instance.Pop(_destroyVFXType) as VFXPlayer;
            vfx.transform.position = transform.position;
            _stateMachine.ChangeState("Dead");
            vfx.Play();
            OnDieEvent?.Invoke(this);
        }

        public virtual void SetLevel(int level)
        {
            EnemyLevel = level;
            OnLevelSetEvent?.Invoke(level);
        }
        public virtual void OnGenerated()
        {
            _stateMachine.ChangeState("Idle");
            _enemyRenderer.SetDeadColor(false);
            _enemyRenderer.SetDissolveLevel(1f);
            OnGeneratedEvent?.Invoke();
        }
    }

}