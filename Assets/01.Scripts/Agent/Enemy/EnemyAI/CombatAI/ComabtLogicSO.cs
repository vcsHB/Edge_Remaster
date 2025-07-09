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
        [SerializeField] protected float _attackCooltime = 0.5f;
        
        protected Enemy _owner;
        protected EnemyAI _enemyAI;
        protected DetectData _targetData;

        protected EnemyWeapon _currentEnemyWeapon;


        #region External Functions


        public virtual void HandleDetect(DetectData detectData)
        {
            _targetData = detectData;
        }

        #endregion


        public virtual void Initialize(Enemy owner, EnemyAI enemyAI)
        {
            _owner = owner;
            _currentEnemyWeapon = Instantiate(_enemyWeaponPrefab, _owner.transform);
            _currentEnemyWeapon.SetLevel(owner.EnemyLevel);
            owner.OnLevelSetEvent += HandleLevelSet;
        }

        private void HandleLevelSet(int newLevel)
        {
            if (_currentEnemyWeapon == null)
            {
                Debug.LogError("Not Exist Enemy own Weapon.");
                return;
            }
        }

        public virtual void UpdateLogic()
        {

        }

        protected abstract void Attack();

        public ComabtLogicSO Clone() => Instantiate(this);
    }
}