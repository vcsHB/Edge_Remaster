using System;
using UnityEngine;
namespace Agents.Enemies.AI
{

    [CreateAssetMenu(menuName ="SO/EnemyAI/Combat/MoveAttackLogic")]
    public class MoveAttackLogicSO : ComabtLogicSO
    {
        public override void Initialize(Enemy owner, EnemyAI enemyAI)
        {
            base.Initialize(owner, enemyAI);
            
            enemyAI.MoveLogic.OnMovementEvent += HandleMovement;
        }

        private void HandleMovement()
        {
            
        }

        protected override void Attack()
        {
            _currentEnemyWeapon.HandleAttack();
        }
    }
}