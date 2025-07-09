    using UnityEngine;
namespace Agents.Enemies.AI
{

    public class ShooterLogicSO : ComabtLogicSO
    {
        

        protected override void Attack()
        {
            _currentEnemyWeapon.HandleAttack();
        }
    }
}