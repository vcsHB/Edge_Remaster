using System;
using Combat;

namespace Agents.Enemies
{
    public class Enemy : Agent
    {
        public int EnemyLevel { get; protected set; }
        public event Action<Enemy> OnDieEvent;
        public event Action<int> OnLevelSetEvent;
        public Health HealthCompo { get; protected set; }
        protected override void Awake()
        {
            base.Awake();
            HealthCompo = GetComponent<Health>();
            HealthCompo.OnDieEvent.AddListener(HandleEnemyDie);
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