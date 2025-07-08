using Combat.CombatObjects;
using UnityEngine;
namespace BuildSystem.Structures
{

    public class BarrierProjector : EnergyRequireStructure
    {
        [SerializeField] private Barrier _barrier;


        protected override void Awake()
        {
            base.Awake();

        }

        public override void HandleWaveStart()
        {
            base.HandleWaveStart();
            float remain = _barrier.TryRepairBarrier(_battery.CurrentEnergy * 2);
            _battery.SetEnergy(remain * 0.5f);
        }

    }
}