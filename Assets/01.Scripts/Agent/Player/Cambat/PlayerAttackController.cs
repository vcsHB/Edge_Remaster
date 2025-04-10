using UnityEngine;

namespace Agents.Players.Combat
{

    public class PlayerAttackController : MonoBehaviour, IAgentComponent
    {
        private Player _player;

        [SerializeField] private PlayerWeapon[] _weapons;
        [SerializeField] private Transform _weaponHandleTrm;
        [SerializeField] private float _weaponDistance = 1.3f;

        private int _currentWeaponIndex = -1;
        private PlayerWeapon CurrentWeapon => _weapons[_currentWeaponIndex];

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            //_player.PlayerInput.OnAttackEvent += HandleAttackEvent;

            foreach (PlayerWeapon weapon in _weapons)
            {
                weapon.Initialize(_player);
            }
            SelectWeapon(0);
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }



        public void SelectWeapon(int index)
        {
            if (_currentWeaponIndex != -1)
            {
                CurrentWeapon.SetDisable();
            }
            _currentWeaponIndex = index;
            CurrentWeapon.SetEnabled();
        }
        private void Update()
        {
            Vector2 mousePos = _player.PlayerInput.MousePosition;
            Vector2 direction = mousePos - (Vector2)transform.position;
            direction.Normalize();

            _weaponHandleTrm.localPosition = direction * _weaponDistance;
        }



        private void HandleAttackEvent() // 폐기
        {
            float damage = _player.PlayerStatus.attackDamage.GetValue();
            Vector2 mousePos = _player.PlayerInput.MousePosition;
            Vector2 direction = mousePos - (Vector2)transform.position;
            direction.Normalize();
            CurrentWeapon.transform.localPosition = direction * _weaponDistance;
            CurrentWeapon.Attack(damage, 10f);

        }
    }

}