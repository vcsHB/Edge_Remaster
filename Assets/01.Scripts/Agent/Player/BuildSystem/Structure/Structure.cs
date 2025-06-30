using System;
using Combat;
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

        public virtual void DestroyStructure()
        {
            OnDestroyEvent?.Invoke(this);
            VFXPlayer vfx = PoolManager.Instance.Pop(ObjectPooling.PoolingType.StructureDestroyVFX) as VFXPlayer;
            vfx.transform.position = transform.position;
            vfx.Play();
            Destroy(gameObject);
        }
    }
}