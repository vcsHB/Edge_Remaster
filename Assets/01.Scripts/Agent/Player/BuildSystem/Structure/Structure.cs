using System;
using Combat;
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

        }

        public virtual void HandleStructureSelected()
        {

        }
        
        public virtual void HandleStructureUnselected()
        {

        }
    }
}