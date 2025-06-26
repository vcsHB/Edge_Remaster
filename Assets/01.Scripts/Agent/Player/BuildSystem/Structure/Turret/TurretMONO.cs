using BuildSystem.Structures.Turrets;
using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
namespace BuildSystem.Structures
{

    public class TurretMONO : TurretStructure
    {

        [SerializeField] private ProjectileShooter[] _projectileShooters;
        
        public override void Fire()
        {
        }
    }
}