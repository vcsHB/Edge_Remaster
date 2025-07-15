using UnityEngine;
namespace BuildSystem.DataManage
{
    [CreateAssetMenu(menuName = "SO/Datas/DataGroup")]

    public class DataGroupSO : ScriptableObject
    {
        public DataSO[] datas;
        

#if UNITY_EDITOR

        private void OnValidate()
        {
            for (int i = 0; i < datas.Length; i++)
            {
                datas[i].SetID(i);
            }
        }
#endif
    }
}