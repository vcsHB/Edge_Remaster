using System.Linq;
using UnityEngine;
namespace BuildSystem.Structures
{

    public class Pump : Structure
    {
        [SerializeField] private LayerMask _structureLayer;
        [SerializeField] private Vector2 _detectArea;
        [Header("Pump Setting")]
        [SerializeField] private float _fillEnergyAmount = 10f;

        public void PumpEnergy()
        {
            Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, _detectArea, 0, _structureLayer);
            IEnergyRestorable[] energyOwners = targets
                .Select(collider => collider.GetComponent<IEnergyRestorable>())
                .Where(component => component != null)
                .ToArray();

            int targetAmout = energyOwners.Length;
            if (targetAmout == 0) return;
            float distributedEnergy = _fillEnergyAmount / targetAmout;
            for (int i = 0; i < targetAmout; i++)
            {
                energyOwners[i].RestoreEnergy(distributedEnergy);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _detectArea);
        }
    }
}