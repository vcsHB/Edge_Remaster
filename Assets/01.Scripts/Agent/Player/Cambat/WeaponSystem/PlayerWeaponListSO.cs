using UnityEngine;
namespace Agents.Players.Combat
{
    [CreateAssetMenu(menuName = "SO/PlayerWeapon/WeaponList")]
    public class PlayerWeaponListSO : ScriptableObject
    {
        [SerializeField] private PlayerWeaponSO[] weaponList;

        public PlayerWeaponSO GetWeaponData(int id)
        {
            if (id < 0 || id >= weaponList.Length)
            {
                Debug.LogError($"[!] Invalid ID Exception. index:{id}");
                return null;
            }
            return weaponList[id];
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (weaponList == null) return;

            for (int i = 0; i < weaponList.Length; i++)
            {
                if (weaponList[i] == null)
                {
                    Debug.LogError($"[!] Null values ​​in the list Exception. index:{i}");
                    continue;
                }
                weaponList[i].SetId(i);
            }
        }
#endif
    }
}