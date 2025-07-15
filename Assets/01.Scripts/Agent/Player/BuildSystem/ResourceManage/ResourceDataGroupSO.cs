using System.Collections.Generic;
using UnityEngine;
namespace BuildSystem.ResourceManage
{
    [CreateAssetMenu(menuName = "SO/Datas/ResourceDataGroup")]

    public class ResourceDataGroupSO : ScriptableObject
    {
        public ResourceDataSO[] datas;
        private Dictionary<ResourceType, ResourceDataSO> _cachDictionary;

        public ResourceDataSO GetData(ResourceType resourceType)
        {
            if (_cachDictionary == null)
            {
                Debug.LogError("cachDictionary is null. not initialized");
                return null;
            }
            return _cachDictionary[resourceType];
        }

#if UNITY_EDITOR

        private void OnValidate()
        {

            _cachDictionary = new();
            for (int i = 0; i < datas.Length; i++)
            {
                if (!_cachDictionary.TryAdd(datas[i].resourceType, datas[i]))
                    Debug.LogError($"Warning! There is wrong ResourceType. type is {datas[i].resourceType.ToString()}");
            }
        }
        
#endif
    }
}