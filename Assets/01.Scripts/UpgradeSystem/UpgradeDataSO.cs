using Core.attribute;
using UnityEngine;
namespace UpgradeSystem
{
    [CreateAssetMenu(menuName = "SO/UpgradeSystem/UpgradeData")]
    public class UpgradeDataSO : ScriptableObject
    {
        [ReadOnly] public int id;
        public Sprite upgradeIcon;
        public string upgradeName;
        public string upgradeDescription;
        public UpgradeEffect[] effects;
        
        [Space(10f)]
        [Header("Unlock Settings")]
        public int requireVertexCost;
        public int requireMetaCost;
        public UpgradeDataSO[] unlockList;

        public void SetId(int newID)
        {
            id = newID;
        }
    }
}