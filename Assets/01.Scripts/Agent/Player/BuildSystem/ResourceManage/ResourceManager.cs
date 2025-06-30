using System;
using UnityEngine;
namespace BuildSystem.ResourceManage
{

    public class ResourceManager : MonoSingleton<ResourceManager>
    {
        [SerializeField] private SerializeDictionary<ResourceType, int> _resourceDictionary;


        protected override void Awake()
        {
            base.Awake();
            _resourceDictionary = new();
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                _resourceDictionary.Add(type, 0);
            }
        }

        public int GetAmount(ResourceType type)
        {
            return _resourceDictionary[type];
        }

        public bool TryUseResources(ResourceData[] requires)
        {
            for (int i = 0; i < requires.Length; i++)
                if (!IsEnough(requires[i].type, requires[i].amount))
                    return false;

            for (int i = 0; i < requires.Length; i++)
                UseResource(requires[i].type, requires[i].amount);

            return true;
        }


        public bool IsEnough(ResourceType type, int amount)
        {
            return GetAmount(type) >= amount;
        }
        public void UseResource(ResourceType type, int amount)
        {
            if (IsEnough(type, amount))
                _resourceDictionary[type] -= amount;
        }

        public void GainResource(ResourceType type, int amount)
        {
            _resourceDictionary[type] += amount;
        }
    }
}