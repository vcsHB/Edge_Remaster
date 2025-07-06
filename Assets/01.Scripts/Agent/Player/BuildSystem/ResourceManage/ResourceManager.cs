using System;
using UnityEngine;
namespace BuildSystem.ResourceManage
{
    [Serializable]
    public class ResourceDataTable
    {
        public Action<int> OnAmountIncreaseEvent;
        public Action<int> OnAmountDecreaseEvent;
        public Action<int> OnAmountChangedEvent;
        public int amount;

        public void ApplyAmount(int amount)
        {
            this.amount += amount;
            OnAmountChangedEvent?.Invoke(this.amount);
            OnAmountIncreaseEvent?.Invoke(this.amount);
        }

        public void ReduceAmount(int amount)
        {
            this.amount -= amount;
            OnAmountChangedEvent?.Invoke(this.amount);
            OnAmountDecreaseEvent?.Invoke(this.amount);
        }

        public ResourceDataTable(int initAmount = 0)
        {
            amount = initAmount;
        }

    }
    public class ResourceManager : MonoSingleton<ResourceManager>
    {
        [SerializeField] private SerializeDictionary<ResourceType, ResourceDataTable> _resourceDictionary;


        protected override void Awake()
        {
            base.Awake();
            _resourceDictionary = new();
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                _resourceDictionary.Add(type, new()); // TODO/ SAVE LOAD
            }
        }

        public ResourceDataTable GetDataTable(ResourceType type)
        {
            return _resourceDictionary[type];
        }

        public int GetAmount(ResourceType type)
        {
            return GetDataTable(type).amount;
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
                _resourceDictionary[type].ReduceAmount(amount);
        }

        public void GainResource(ResourceType type, int amount)
        {
            _resourceDictionary[type].ApplyAmount(amount);
        }
    }
}