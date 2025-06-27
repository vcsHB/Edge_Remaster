using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
namespace BuildSystem.Structures.Turrets
{

    public class TurretDUO : TurretStructure
    {
        [SerializeField] private ProjectileShooter[] _projectileShooters;
        private int _currentFireIndex;
        public override void Fire()
        {

            _projectileShooters[_currentFireIndex].FireProjectile(_mainHead.transform.up);
            _currentFireIndex = (_currentFireIndex + 1) % _projectileShooters.Length;
        }
    }
}