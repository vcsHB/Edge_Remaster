using BuildSystem.Structures.Turrets;
using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
namespace BuildSystem.Structures
{

    public class TurretMONO : TurretStructure
    {

        [SerializeField] private ProjectileShooter _projectileShooter;

        public override void Fire()
        {

            _projectileShooter.FireProjectile(_mainHead.transform.up);
        }
    }
}