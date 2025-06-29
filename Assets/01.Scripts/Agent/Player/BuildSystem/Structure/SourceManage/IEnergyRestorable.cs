using UnityEngine;
namespace BuildSystem.Structures
{

    public interface IEnergyRestorable
    {
        public void RestoreEnergy(float amount);

        public void SetHighlight(bool value); 
        
    }
}