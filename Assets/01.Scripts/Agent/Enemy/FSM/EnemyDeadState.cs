namespace Agents.Enemies.FSM
{
    public class EnemyDeadState : EnemyState
    {
        protected PoolableEnemy _poolableEnemy;
        public EnemyDeadState(Enemy owner, EnemyStateMachine stateMachine, int animationParam) : base(owner, stateMachine, animationParam)
        {
            _poolableEnemy = owner as PoolableEnemy;
        }

        public override void Enter()
        {
            base.Enter();
            _renderer.StartDissolve(HandleDissolveOver);
        }

        private void HandleDissolveOver()
        {
            _poolableEnemy.HandleReturnToPool();
        }
    }
}