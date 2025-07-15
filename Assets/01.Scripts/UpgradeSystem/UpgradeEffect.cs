using UnityEngine;
namespace UpgradeSystem
{
    //[CreateAssetMenu(menuName ="SO/UpgradeSystem/UpgradeEffect")]
    public abstract class UpgradeEffect : ScriptableObject
    {
        public abstract void ApplyEffect();

    }
}