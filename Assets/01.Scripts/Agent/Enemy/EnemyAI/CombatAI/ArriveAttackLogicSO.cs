using System;
using UnityEngine;
namespace Agents.Enemies.AI.Combat
{
    [CreateAssetMenu(menuName = "SO/EnemyAI/Combat/ArriveAttackLogic")]
    public class ArriveAttackLogicSO : ComabtLogicSO
    {
        /*
        *  # Properties in parent
        *  protected EnemyWeapon _currentEnemyWeapon;
        */

        public override void Initialize(Enemy owner, EnemyAI enemyAI)
        {
            base.Initialize(owner, enemyAI);
            _enemyAI.MoveLogic.OnArriveEvent += HandleLocationArrived;

        }

        private void HandleLocationArrived()
        {
            Attack();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();


        }

        protected override void Attack()
        {
            _currentEnemyWeapon.HandleAttack();
        }
    }
}