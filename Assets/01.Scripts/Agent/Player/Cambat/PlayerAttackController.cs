using System;
using UnityEngine;

namespace Agents.Players.Combat
{

    public class PlayerAttackController : MonoBehaviour, IAgentComponent
    {
        private Player _player;

        [SerializeField] private PlayerWeaponSO _currentWeaponSO;
        [SerializeField] private PlayerWeapon _currentWeapon;
        [SerializeField] private Transform _weaponHandleTrm;
        [SerializeField] private float _weaponDistance = 1.3f;


        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            //_player.PlayerInput.OnAttackEvent += HandleAttackEvent;

            _currentWeapon = Instantiate(_currentWeaponSO.weaponPrefab, transform);
            _currentWeapon.Initialize(_player);
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }



        private void Update()
        {
            Vector2 mousePos = _player.PlayerInput.MousePosition;
            Vector2 direction = mousePos - (Vector2)transform.position;
            direction.Normalize();

            _weaponHandleTrm.localPosition = direction * _weaponDistance;
        }

        #region Obsolete Functions

        [Obsolete("Discarded Functions")]
        public void SelectWeapon(int index)
        {
            // if (_currentWeaponIndex != -1)
            // {
            //     CurrentWeapon.SetDisable();
            // }
            // _currentWeaponIndex = index;
            // CurrentWeapon.SetEnabled();
        }

        [Obsolete("Discarded Functions")]
        private void HandleAttackEvent() // 폐기
        {
            // float damage = _player.PlayerStatus.attackDamage.GetValue();
            // Vector2 mousePos = _player.PlayerInput.MousePosition;
            // Vector2 direction = mousePos - (Vector2)transform.position;
            // direction.Normalize();
            // _currentWeapon.transform.localPosition = direction * _weaponDistance;
            // _currentWeapon.Attack(damage, 10f);

        }
        #endregion
    }

}