using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{

    [CreateAssetMenu(menuName = "SO/Status/Stat")]
    public class Stat : ScriptableObject
    {
        [Header("Display Setting")]
        public string statName;
        public string description;
        public string displayName;
        [SerializeField] private Sprite _icon;

        [Header("Value")]
        [SerializeField] private float _baseValue, _minValue, _maxValue;
        public List<float> modifier;
        public Sprite Icon => _icon;

        public float BaseValue => _baseValue;
        public float MaxValue => _maxValue;
        public float MinValue => _minValue;


        private bool _isValueChanged = true;
        private float _cashedValue;


        public float GetValue()
        {
            if (!_isValueChanged) return _cashedValue;


            float result = _baseValue;
            for (int i = 0; i < modifier.Count; i++)
            {
                result += modifier[i];
            }
            _cashedValue = result;
            _isValueChanged = false;
            return result;
        }


        public void AddModifier(float value)
        {
            modifier.Add(value);
            _isValueChanged = true;
        }

        public void RemoveModifier(float value)
        {
            modifier.Remove(value);
            _isValueChanged = true;
        }
        public virtual object Clone() => Instantiate(this);
    }
}