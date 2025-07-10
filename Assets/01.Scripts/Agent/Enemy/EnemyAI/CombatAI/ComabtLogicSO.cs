using System;
using Agents.Enemies.AI.Weapons;
using Agnets.Enemies;
using UnityEngine;
namespace Agents.Enemies.AI
{

    public abstract class ComabtLogicSO : ScriptableObject
    {
        [SerializeField] private EnemyWeapon _enemyWeaponPrefab;
        [Header("Cooltime Setting")]

        protected Enemy _owner;
        protected EnemyAI _enemyAI;
        protected DetectData _targetData;

        protected EnemyWeapon _currentEnemyWeapon;


        #region External Functions


        public virtual void HandleDetect(DetectData detectData)
        {
            _targetData = detectData;
            _currentEnemyWeapon.SetTarget(detectData.targetCollider);
        }

        #endregion


        public virtual void Initialize(Enemy owner, EnemyAI enemyAI)
        {
            _owner = owner;
            _enemyAI = enemyAI;
            _currentEnemyWeapon = Instantiate(_enemyWeaponPrefab, _owner.transform);
            _currentEnemyWeapon.SetOwner(owner);
            _currentEnemyWeapon.SetLevel(owner.EnemyLevel);
            owner.OnLevelSetEvent += HandleLevelSet;
            _enemyAI.DetectLogic.OnDetectEvent += HandleDetect;
        }

        private void HandleLevelSet(int newLevel)
        {
            if (_currentEnemyWeapon == null)
            {
                Debug.LogError("Not Exist Enemy own Weapon.");
                return;
            }
            _currentEnemyWeapon.SetLevel(newLevel);
        }

        public virtual void UpdateLogic()
        {

        }

        protected abstract void Attack();

        public ComabtLogicSO Clone() => Instantiate(this);
    }
}