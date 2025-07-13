using UI.TitleScene;
using UnityEngine;
namespace UIManage.TitleScene
{

    public class UpgradeRequireCostPanel : MonoBehaviour
    {
        [SerializeField] private UpgradeRequireCostSlot[] _slots;

        public void SetAmountData(int[] current, int[] require)
        {
            if (require.Length != 2)
            {
                Debug.LogWarning("Require Cost Amount array format is invalid");
                return;
            }
            for (int i = 0; i < require.Length; i++)
            {
                _slots[i].SetAmount(current[i], require[i]);
            }
        }
    }
}