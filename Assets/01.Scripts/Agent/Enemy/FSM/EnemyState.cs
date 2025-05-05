using UnityEditor.MPE;
using UnityEngine;
namespace Agents.Enemies.FSM
{

    public class EnemyState
    {
        protected EnemyMovement _mover;
        protected EnemyRenderer _renderer;
        protected EnemyAI _enemyAI;
        protected Enemy _enemy;
        protected EnemyStateMachine _stateMachine;
        protected bool _isTriggered;
        protected int _animParam;


        public EnemyState(Enemy owner, EnemyStateMachine stateMachine, int animationParam)
        {
            _enemy = owner;
            _stateMachine = stateMachine;
            _renderer = owner.GetCompo<EnemyRenderer>();
            _mover = owner.GetCompo<EnemyMovement>();
            _enemyAI = owner.GetCompo<EnemyAI>();
            _animParam = animationParam;
        }

        public virtual void Enter()
        {
            _isTriggered = false;
            _renderer.SetParam(_animParam, true);

        }

        public virtual void Update()
        {

        }

        public virtual void Exit()
        {
            _renderer.SetParam(_animParam, false);
        }


    }
}