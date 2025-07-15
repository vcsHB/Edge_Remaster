using UnityEngine;
namespace UpgradeSystem
{
    [CreateAssetMenu(menuName = "SO/UpgradeSystem/ScreenScaleUpgradeEffect")]
    public class ScreenScaleUpgrade : UpgradeEffect
    {
        [SerializeField] private Vector2Int _overrideScreenSize = new Vector2Int(1080, 1080);
        public override void ApplyEffect()
        {
            if (_overrideScreenSize.x >= Screen.currentResolution.width && _overrideScreenSize.y >= Screen.currentResolution.height)
            {
                Screen.SetResolution(_overrideScreenSize.x, _overrideScreenSize.y, false);

            }
        }
    }
}