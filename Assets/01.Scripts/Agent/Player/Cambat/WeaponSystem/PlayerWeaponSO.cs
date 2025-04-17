using UnityEngine;
namespace Agents.Players.Combat
{

    [CreateAssetMenu(menuName = "SO/PlayerWeapon/Weapon")]
    public class PlayerWeaponSO : ScriptableObject
    { // 무기 = 플레이어 타입
        [field:SerializeField] public int id;
        public string weaponName;
        public string description;
        public Sprite weaponIcon;
        public Color personalColor;
        public PlayerWeapon weaponPrefab;

        internal void SetId(int newId)
        {
            id = newId;
        }
    }
}