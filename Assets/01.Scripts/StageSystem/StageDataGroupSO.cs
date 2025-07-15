using UnityEngine;
namespace StageSystem
{
    [CreateAssetMenu(menuName = "SO/Stage/StageDataGroupSO")]
    public class StageDataGroupSO : ScriptableObject
    {
        public StageDataSO[] datas;

        public StageDataSO GetData(int id)
        {
            if (id < 0 || id > datas.Length - 1)
            {
                Debug.LogError($"Stage Data Id is invalid. ID:{id}");
                return null;
            }
            return datas[id];
        }



#if UNITY_EDITOR

        private void OnValidate()
        {
            if (datas == null) return;
            for (int i = 0; i < datas.Length; i++)
            {
                datas[i].SetID(i);
            }
        }
#endif
    }
}