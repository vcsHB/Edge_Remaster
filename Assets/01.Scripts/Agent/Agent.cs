using System;
using System.Collections.Generic;
using System.Linq;
using Core.EventSystem;
using UnityEngine;

namespace Agents
{

    public class Agent : MonoBehaviour
    {

        private Dictionary<Type, IAgentComponent> _components = new Dictionary<Type, IAgentComponent>();
        [field: SerializeField] public GameEventChannelSO EventChannel { get; set; }
        protected virtual void Awake()
        {
            EventChannel = Instantiate(EventChannel);
            AddComponentToDictionary();
            ComponentInitialize();
            AfterInit();

        }

        private void AddComponentToDictionary()
        {
            GetComponentsInChildren<IAgentComponent>(true)
                .ToList().ForEach(compo => _components.Add(compo.GetType(), compo));
        }

        private void ComponentInitialize()
        {
            _components.Values.ToList().ForEach(compo => compo.Initialize(this));
        }

        private void AfterInit()
        {
            _components.Values.ToList().ForEach(compo => compo.AfterInit());
        }

        public T GetCompo<T>(bool allowDerived = false) where T : class
        {
            Type targetType = typeof(T);

            // 1. Exact type matching cache lookup
            if (_components.TryGetValue(targetType, out IAgentComponent cached))
            {
                return cached as T;
            }

            // 2.Try GetComponentInChildren if it doesn't exist
            T found = GetComponentInChildren<T>();
            if (found is IAgentComponent agentComponent)
            {
                _components[targetType] = agentComponent;
                return found;
            }

            // 3. Search for child classes when inheritance type is allowed
            if (allowDerived)
            {
                Type derivedKey = _components.Keys.FirstOrDefault(k => k.IsSubclassOf(targetType));
                if (derivedKey != null && _components[derivedKey] is T derivedCompo)
                {
                    return derivedCompo;
                }
            }

            return default;
        }

    }

}