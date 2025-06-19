using Combat;
using UnityEngine;
namespace BuildSystem
{

    public class Structure : MonoBehaviour
    {
        [field: SerializeField] public StructureDataSO DataSO { get; private set; }
        public Health HealthCompo { get; private set; }


        protected virtual void Awake()
        {

        }
        
        
    }
}