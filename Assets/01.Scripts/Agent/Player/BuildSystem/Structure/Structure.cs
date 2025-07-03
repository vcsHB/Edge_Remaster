using System;
using System.Collections.Generic;
using Combat;
using Core.attribute;
using ObjectManage;
using UnityEngine;
namespace BuildSystem.Structures
{
    public struct StructureProperties
    {
        public float health;
        public float energy;
    }
    [RequireComponent(typeof(Health))]
    public class Structure : MonoBehaviour
    {
        [field: SerializeField] public StructureDataSO DataSO { get; private set; }
        public Health HealthCompo { get; private set; }
        public event Action<Structure> OnDestroyEvent;
        [ReadOnly] public float baseSpeed = 1f;

        public float WorkSpeed
        {
            get
            {
                if (_isSpeedValueChanged)
                {
                    _workSpeed = baseSpeed;
                    for (int i = 0; i < _modifiers.Count; i++)
                        _workSpeed += _modifiers[i];
                    _isSpeedValueChanged = false;
                }
                return _workSpeed;
            }
        }
        private float _workSpeed;
        private bool _isSpeedValueChanged = true;

        private List<float> _modifiers = new();



        protected virtual void Awake()
        {
            HealthCompo = GetComponent<Health>();
            HealthCompo.OnDieEvent.AddListener(HandleStructDestroyEvent);
        }
        public virtual void SetStructureProperty(StructureProperties properties)
        {
            HealthCompo.SetCurrentHealth(properties.health);
        }

        private void HandleStructDestroyEvent()
        {
            DestroyStructure();
        }

        public virtual void HandleStructureSelected()
        {

        }

        public virtual void HandleStructureUnselected()
        {

        }

        public virtual void HandleWaveStart()
        {

        }

        public virtual void DestroyStructure()
        {
            OnDestroyEvent?.Invoke(this);
            VFXPlayer vfx = PoolManager.Instance.Pop(ObjectPooling.PoolingType.StructureDestroyVFX) as VFXPlayer;
            vfx.transform.position = transform.position;
            vfx.Play();
            Destroy(gameObject);
        }

        #region Speed modifers

        public void AddSpeedModifier(float value)
        {
            _modifiers.Add(value);
            _isSpeedValueChanged = true;

        }

        public void RemoveSpeedModifier(float value)
        {
            if (_modifiers.Contains(value))
            {
                _modifiers.Remove(value);
                _isSpeedValueChanged = true;
            }
        }

        #endregion
    }
}