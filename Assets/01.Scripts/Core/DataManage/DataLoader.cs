using UnityEngine;
using UpgradeSystem;
namespace Core.DataManage
{

    public class DataLoader : MonoBehaviour
    {
        [SerializeField] private UpgradeDataGroupSO _upgradeDataGroup;

        private void Start()
        {
            DataManager.Load();

            var datas = DataManager.upgradeData.unlockDatas;
            for (int i = 0; i < datas.Count; i++)
            {
                UpgradeDataSO upgrade = _upgradeDataGroup.GetData(datas[i]);
                foreach (var effect in upgrade.effects)
                {
                    effect.ApplyEffect();
                }
            }

        }
    }
}