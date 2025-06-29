using System;
using Combat;
using ObjectManage;
using UnityEngine;
namespace BuildSystem.Structures
{
    [RequireComponent(typeof(Health))]
    public class Structure : MonoBehaviour
    {
        [field: SerializeField] public StructureDataSO DataSO { get; private set; }
        public Health HealthCompo { get; private set; }



        protected virtual void Awake()
        {
            HealthCompo = GetComponent<Health>();
            HealthCompo.OnDieEvent.AddListener(HandleStructDestroyEvent);
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
            VFXPlayer vfx = PoolManager.Instance.Pop(ObjectPooling.PoolingType.StructureDestroyVFX) as VFXPlayer;
            vfx.transform.position = transform.position;
            vfx.Play();
            Destroy(gameObject);
        }
    }
}