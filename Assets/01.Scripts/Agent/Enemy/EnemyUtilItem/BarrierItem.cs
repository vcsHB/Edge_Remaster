using Combat.CombatObjects;
using UnityEngine;
namespace Agents.Enemies.UtilItem
{

    public class BarrierItem : EnemyItem
    {
        [SerializeField] private Barrier _barrier;


        public override void ResetItem()
        {

            _barrier.SetMaxDurability();

        }
    }
}