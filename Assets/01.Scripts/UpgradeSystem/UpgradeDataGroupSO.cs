using UnityEngine;
namespace UpgradeSystem
{

    [CreateAssetMenu(menuName = "SO/UpgradeSystem/UpgradeDataGroupSO")]
    public class UpgradeDataGroupSO : ScriptableObject
    {
        public UpgradeDataSO[] datas;

        public UpgradeDataSO GetData(int id)
        {
            if (id < 0 || id > datas.Length - 1)
            {
                Debug.LogError($"Upgrade Data Id is invalid. ID:{id}");
                return null;
            }
            return datas[id];
        }



#if UNITY_EDITOR

        private void OnValidate()
        {
            for (int i = 0; i < datas.Length; i++)
            {
                datas[i].SetId(i);
            }
        }
#endif
    }
}